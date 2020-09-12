using System;

namespace Trader.Domain.Filter
{
    public class GetStockNegotiationFilter
    {
        public GetStockNegotiationFilter(string stockCode, DateTime initialDate, DateTime finalDate)
        {
            StockCode = stockCode;
            InitialDate = initialDate;
            FinalDate = finalDate;
        }

        public string StockCode { get; set; }

        public DateTime InitialDate { get; set; }

        public DateTime FinalDate { get; set; }
    }
}
