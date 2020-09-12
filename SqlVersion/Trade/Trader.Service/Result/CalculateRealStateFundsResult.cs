using System.Collections.Generic;

namespace Trader.Service.Result
{
    public class CalculateRealStateFundsResult : BaseResult
    {
        public CalculateRealStateFundsResult()
        {
            Statistics = new List<RealStateFundsStatisticsResult>();
        }

        public List<RealStateFundsStatisticsResult> Statistics { get; set; }
    }
}
