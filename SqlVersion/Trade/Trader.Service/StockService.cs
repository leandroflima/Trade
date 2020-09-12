using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trader.Domain.Entities;
using Trader.Domain.Filter;
using Trader.Infra.Repositories;
using Trader.Service.Mapper;
using Trader.Service.Param;
using Trader.Service.Result;
using Trader.Service.Validator;

namespace Trader.Service
{
    public class StockService
    {
        public void Import(ImportParam importParam)
        {
            var validate = new ImportParamValidator().Validate(importParam);
            if (!validate.IsValid)
            {
                throw new ApplicationException($"Falha nos parâmetros de entrada ({string.Join(",", validate.Errors)})");
            }

            var repository = new BovespaRepository();

            var stockNegotiationList = repository.ImportFileHistory(importParam.BasePath, importParam.FileName);

            var stockRepository = new StockRepository();

            var stockDic = new Dictionary<string, Stock>();
            foreach (var stockNegotiation in stockNegotiationList)
            {
                if (!stockDic.ContainsKey(stockNegotiation.Stock.Code))
                {
                    stockDic.Add(stockNegotiation.Stock.Code, stockNegotiation.Stock);
                }
            }

            var stockList = stockDic.Select(a => a.Value).ToList();

            stockRepository.BulkMerge(stockList);

            stockRepository.BulkMerge(stockNegotiationList);
        }

        public List<StockAverage> CalculateAverage(CalculateAverageParam param)
        {
            var validate = new CalculateAverageParamValidator().Validate(param);
            if (!validate.IsValid)
            {
                throw new ApplicationException($"Falha nos parâmetros de entrada ({string.Join(",", validate.Errors)})");
            }

            var stockRepository = new StockRepository();

            var getAllStocksFilter = new GetAllStocksFilter(param.MarketCode, param.SpecificationCode, param.BdiCode);

            var stockList = stockRepository.GetAllStocks(getAllStocksFilter);

            if (!string.IsNullOrEmpty(param.Code))
            {
                stockList = stockList.Where(a => string.Compare(a.Code, param.Code, true) == 0).ToList();
            }

            var po = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

            var averageResults = new ConcurrentBag<StockAverage>();

            Parallel.ForEach(stockList, po, (stock) =>
            {
                try
                {
                    var getStockNegotiationFilter = new GetStockNegotiationFilter(stock.Code, param.InitialDate, param.FinalDate);

                    var stockNegotiationList = stockRepository.GetStockNegotiation(getStockNegotiationFilter);

                    stockNegotiationList = stockNegotiationList.OrderBy(a => a.Date).ToList();

                    var averageValue = 0M;
                    var daysCount = 0;
                    var values = new List<StockDayValue>();
                    var lastValue = 0M;
                    var percentageAboveAverage = 0M;
                    var percentageBelowAverage = 0M;
                    var amountOfTradeInLastDay = 0;

                    if (stockNegotiationList.Count() > 0)
                    {
                        averageValue = stockNegotiationList.Average(a => a.LastValue);
                        daysCount = stockNegotiationList.Count();
                        values = stockNegotiationList.ConvertAll(a => new StockDayValue { Day = a.Date.Date, Value = a.LastValue, AmountOfTrade = a.AmountOfTrade });
                        var maxDate = values.Max(b => b.Day.Date);
                        var lastStockDayValue = values.First(a => a.Day.Date.Equals(maxDate));

                        lastValue = lastStockDayValue.Value;
                        amountOfTradeInLastDay = lastStockDayValue.AmountOfTrade;

                        if (lastStockDayValue.Value > averageValue)
                        {
                            percentageAboveAverage = ((lastStockDayValue.Value - averageValue) / averageValue);
                        }
                        else if (lastStockDayValue.Value < averageValue)
                        {
                            percentageBelowAverage = ((averageValue - lastStockDayValue.Value) / averageValue);
                        }
                    }

                    var average = new StockAverage
                    {
                        StockCode = stock.Code,
                        InitialDate = param.InitialDate.Date,
                        FinalDate = param.FinalDate.Date,
                        AverageValue = Math.Round(averageValue, 2),
                        PercentageAboveAverage = Math.Round(percentageAboveAverage, 4),
                        PercentageBelowAverage = Math.Round(percentageBelowAverage, 4),
                        DaysCount = daysCount,
                        Values = values,
                        LastValue = lastValue,
                        AmountOfTradeInLastDay = amountOfTradeInLastDay
                    };

                    averageResults.Add(average);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{stock.Code}");
                    Console.WriteLine(ex.Message);
                }
            });

            return averageResults.ToList();
        }

        public CalculateRealStateFundsResult CalculateRealStateFunds(CalculateRealStateFundsParam param)
        {
            var result = new CalculateRealStateFundsResult();

            var actualYear = DateTime.Now.Year;
            var initialDate = new DateTime((actualYear - 10), 1, 1);
            var finalDate = new DateTime(actualYear, 12, 31);

            var stockRepository = new StockRepository();

            var filter = new GetAllStocksFilter(10, "12");
            var stocks = stockRepository.GetAllStocks(filter);

            foreach (var stock in stocks)
            {
                var getFilter = new GetStockNegotiationFilter(stock.Code, initialDate, finalDate);
                var stockNegotiations = stockRepository.GetStockNegotiation(getFilter);

                var realStateFundsStatistics = new RealStateFundsStatistics(stock, stockNegotiations);

                result.Statistics.Add(realStateFundsStatistics.MapToResult());
            }

            return result;
        }
    }
}
