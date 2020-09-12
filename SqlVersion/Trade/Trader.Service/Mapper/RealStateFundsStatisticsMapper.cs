using Trader.Domain.Entities;
using Trader.Service.Result;

namespace Trader.Service.Mapper
{
    public static class RealStateFundsStatisticsMapper
    {
        public static RealStateFundsStatisticsResult MapToResult(this RealStateFundsStatistics realStateFundsStatistics)
        {
            return new RealStateFundsStatisticsResult(realStateFundsStatistics.Stock.Code,
                realStateFundsStatistics.CountInNegotiation,
                realStateFundsStatistics.FirstNegotiationDate,
                realStateFundsStatistics.LastNegotiationDate,
                realStateFundsStatistics.MaxLastValue,
                realStateFundsStatistics.MinLastValue,
                realStateFundsStatistics.LastValue);
        }
    }
}
