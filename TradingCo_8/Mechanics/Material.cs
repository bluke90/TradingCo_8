using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingCo.Mechanics
{
    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double BasePrice { get; set; }
        public int Quantity { get; set; }
        public string IconName { get; set; }
    }
}
