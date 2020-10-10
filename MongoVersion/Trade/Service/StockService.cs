using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Trade.Domain;

namespace Trade.Service
{
    public class StockService
    {
        public StockService()
        {
        }

        public void PrintAmountOfTradeReport(IEnumerable<StockNegotiation> stocks)
        {
            var plan = new StringBuilder();

            var dates = stocks.Select(stock => stock.Date).Distinct();

            var groupByStock = stocks.ToList().GroupBy(stock => new { stock.Stock.Code });

            var header = new StringBuilder();
            header.Append("Code;");
            foreach (var date in dates)
            {
                header.Append($"{date.ToString("dd/MM/yyyy")};");
            }

            plan.AppendLine(header.ToString());

            foreach (var stock in groupByStock)
            {
                var line = new StringBuilder();
                line.Append($"{stock.Key.Code};");

                var stockList = stock.ToDictionary(item => int.Parse(item.Date.ToString("yyyyMMdd")), item => item.AmountOfTrade);
                foreach (var date in dates)
                {
                    var amountOfTrade = 0M;
                    var dateNumber = int.Parse(date.Date.ToString("yyyyMMdd"));
                    if (stockList.ContainsKey(dateNumber))
                    {
                        amountOfTrade = stockList[dateNumber];
                    }
                    line.Append($"{amountOfTrade};");
                }
                plan.AppendLine(line.ToString());
            }

            var path = Path.Combine(Environment.CurrentDirectory, $"Stocks.Plan.{DateTime.Now.ToString("yyyyMMddHHmmss")}.csv");
            File.WriteAllText(path, plan.ToString());
        }

        public void SearchMostHighStocks(IEnumerable<StockNegotiation> stocks)
        {
            var stocksSorted = stocks.OrderByDescending(stock => stock.Date);

            var stockGroupsByCode = stocksSorted.GroupBy(stock => stock.Stock.Code);

            var counter = 0;

            foreach (var stockGroupByCode in stockGroupsByCode)
            {
                var stockCode = stockGroupByCode.Key;
                var stockNegotiations = stockGroupByCode.ToList();

                var directory = Path.Combine(Environment.CurrentDirectory, "SearchMostHighStocks");
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var file = Path.Combine(directory, $"{stockCode}.csv");

                Console.WriteLine($"{counter} {stockCode}");

                using (var fw = new StreamWriter(file, false, Encoding.Default))
                {
                    fw.AutoFlush = true;
                    fw.WriteLine("Data;" +
                        "Ganho(%);" +
                        "Faixa(%);" +
                        "Qtde Negócios;" +
                        "Qtde Títulos;" +
                        "Valor Médio;" +
                        "Maior Valor;" +
                        "Menor Valor;" +
                        "Primeiro Valor;" +
                        "Último Valor;");
                    foreach (var item in stockNegotiations)
                    {
                        fw.WriteLine($"{item.Date.ToString("dd/MM/yyyy")};" +
                            $"{item.PercentualGain};" +
                            $"{item.PercentualRange};" +
                            $"{item.AmountOfTrade};" +
                            $"{item.AmountOfBonds};" +
                            $"{item.AverageValue};" +
                            $"{item.MaxValue};" +
                            $"{item.MinValue};" +
                            $"{item.FirstValue};" +
                            $"{item.LastValue};");
                    }
                    fw.Close();
                }

                counter += 1;
            }


            var stocksGroup = stocksSorted.GroupBy(stock => stock.Stock);

            foreach (var stockGroup in stocksGroup)
            {
                var stock = stockGroup.Key;

                var negotiations = stockGroup.ToList();

                

            }

        }
    }
}
