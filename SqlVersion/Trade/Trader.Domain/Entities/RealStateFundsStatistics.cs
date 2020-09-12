using System;
using System.Collections.Generic;
using System.Linq;

namespace Trader.Domain.Entities
{
    public class RealStateFundsStatistics
    {
        public RealStateFundsStatistics(Stock stock, List<StockNegotiation> negotiations)
        {
            Stock = stock;
            Negotiations = negotiations;
        }

        public Stock Stock { get; private set; }

        public List<StockNegotiation> Negotiations { get; private set; }

        public int CountInNegotiation
        {
            get
            {
                return Negotiations.Count();
            }
        }

        public DateTime FirstNegotiationDate
        {
            get
            {
                return Negotiations.Min(a => a.Date);
            }
        }

        public DateTime LastNegotiationDate
        {
            get
            {
                return Negotiations.Max(a => a.Date);
            }
        }

        public decimal MaxLastValue
        {
            get
            {
                return Negotiations.Max(a => a.LastValue);
            }
        }

        public decimal MinLastValue
        {
            get
            {
                return Negotiations.Min(a => a.LastValue);
            }
        }

        public decimal LastValue
        {
            get
            {
                var dateOrdered = Negotiations.OrderByDescending(a => a.Date);
                return dateOrdered.First().LastValue;
            }
        }
    }
}
