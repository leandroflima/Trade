using System;
using Trader.Domain.Entities;

namespace Trader.Infra.Models
{
    public class StockNegotiationModel
    {
        public string Code { get; set; }

        public DateTime Date { get; set; }

        public decimal FirstValue { get; set; }

        public decimal MaxValue { get; set; }

        public decimal MinValue { get; set; }

        public decimal AverageValue { get; set; }

        public decimal LastValue { get; set; }

        public decimal BestBuyValue { get; set; }

        public decimal BestSellValue { get; set; }

        public int AmountOfTrade { get; set; }

        public long AmountOfBonds { get; set; }

        public decimal TotalValueOfBonds { get; set; }

        public StockNegotiationModel(StockNegotiation stockNegotiation)
        {
            Code = stockNegotiation.Stock.Code;
            Date = stockNegotiation.Date;
            FirstValue = stockNegotiation.FirstValue;
            MaxValue = stockNegotiation.MaxValue;
            MinValue = stockNegotiation.MinValue;
            AverageValue = stockNegotiation.AverageValue;
            LastValue = stockNegotiation.LastValue;
            BestBuyValue = stockNegotiation.BestBuyValue;
            BestSellValue = stockNegotiation.BestSellValue;
            AmountOfTrade = stockNegotiation.AmountOfTrade;
            AmountOfBonds = stockNegotiation.AmountOfBonds;
            TotalValueOfBonds = stockNegotiation.TotalValueOfBonds;
        }

        public StockNegotiationModel()
        {
        }

        public StockNegotiation ToEntity()
        {
            return new StockNegotiation
            {
                Stock = new Stock { Code = Code },
                Date = Date,
                FirstValue = FirstValue,
                MaxValue = MaxValue,
                MinValue = MinValue,
                AverageValue = AverageValue,
                LastValue = LastValue,
                BestBuyValue = BestBuyValue,
                BestSellValue = BestSellValue,
                AmountOfTrade = AmountOfTrade,
                AmountOfBonds = AmountOfBonds,
                TotalValueOfBonds = TotalValueOfBonds,
            };
        }
    }
}
