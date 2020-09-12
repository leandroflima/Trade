using System;

namespace Trader.Service.Result
{
    public class RealStateFundsStatisticsResult
    {
        public RealStateFundsStatisticsResult(string code, int countInNegotiation, DateTime firstNegotiationDate, DateTime lastNegotiationDate, decimal maxLastValue, decimal minLastValue, decimal lastValue)
        {
            Code = code;
            CountInNegotiation = countInNegotiation;
            FirstNegotiationDate = firstNegotiationDate;
            LastNegotiationDate = lastNegotiationDate;
            MaxLastValue = maxLastValue;
            MinLastValue = minLastValue;
            LastValue = lastValue;
        }

        public string Code { get; set; }

        public int CountInNegotiation { get; set; }

        public DateTime FirstNegotiationDate { get; set; }

        public DateTime LastNegotiationDate { get; set; }

        public decimal MaxLastValue { get; set; }

        public decimal MinLastValue { get; set; }

        public decimal LastValue { get; set; }
    }
}
