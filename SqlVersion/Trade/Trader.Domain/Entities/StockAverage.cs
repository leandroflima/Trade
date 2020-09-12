using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader.Domain.Entities
{
    public class StockAverage
    {
        public string StockCode { get; set; }

        public DateTime InitialDate { get; set; }

        public DateTime FinalDate { get; set; }

        public decimal AverageValue { get; set; }

        public decimal PercentageAboveAverage { get; set; }

        public decimal PercentageBelowAverage { get; set; }

        public int DaysCount { get; set; }

        public decimal LastValue { get; set; }

        public int AmountOfTradeInLastDay { get; set; }

        public List<StockDayValue> Values { get; set; }
    }
}
