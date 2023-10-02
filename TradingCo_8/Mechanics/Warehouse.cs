using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingCo.Mechanics
{
    public class Warehouse
    {
        public int Id { get; set; }
        public int Level { get; set; } = 0;
        public int[] Capacity = { 100, 150, 250, 300, 400, 450 };
        public int CurrentTotal { get; set; }
    }
}
