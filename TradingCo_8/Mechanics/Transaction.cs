using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingCo.Mechanics
{
    public class Transaction
    {
        public int Id { get; set; }
        public Double PreBalance { get; set; }
        public Double PostBalance { get; set; }
        public Double Amount { get; set; }
        public string Material { get; set; }
        public int Quantity { get; set; }
    }
}
