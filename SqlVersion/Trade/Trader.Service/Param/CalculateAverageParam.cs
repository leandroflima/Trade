using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader.Service.Param
{
    public class CalculateAverageParam
    {
        public string Code { get; set; }

        public DateTime InitialDate { get; set; }

        public DateTime FinalDate { get; set; }

        public int? MarketCode { get; set; }

        public string SpecificationCode { get; set; }

        public string BdiCode { get; set; }
    }
}
