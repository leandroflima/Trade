using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader.Domain.Entities
{
    public class StockDayValue
    {
        public DateTime Day { get; set; }

        public decimal Value { get; set; }

        public int AmountOfTrade { get; set; }
    }
}
