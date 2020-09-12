using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader.Domain.Entities
{
    public class CorrectionIndicator
    {
        public int Code { get; set; }

        public string Symbol { get; set; }

        public string Description { get; set; }
    }
}
