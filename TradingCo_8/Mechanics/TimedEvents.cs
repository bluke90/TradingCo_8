using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingCo.Mechanics
{
    public class TimedEvents
    {
        private MaterialStorage MaterialStorage { get; set; }

        private List<TimedEvent> StaticEvents = new List<TimedEvent>()
        {
            new TimedEvent()
            {
                Identifier = "SaturateGoldMarket",
                Material = new Material(){Name = "Gold"},
                CurrentPriceChange = -100,
                BasePriceChange = -75
            },
            new TimedEvent()
            {
                Identifier = "SaturateSilverMarket",
                Material = new Material(){Name = "Silver"},
                CurrentPriceChange = -100,
                BasePriceChange = -75
            },
            new TimedEvent()
            {
                Identifier = "SaturateStoneMarket",
                Material = new Material(){Name = "Stone"},
                CurrentPriceChange = -100,
                BasePriceChange = -75
            }
        };

        public void TriggerEvent(string timedEventId) {
            foreach (var even in StaticEvents) {

            }
        }



    }

    public class TimedEvent
    {
        public string Identifier { get; set; }
        public Material Material { get; set; }
        public double CurrentPriceChange { get; set; }
        public double BasePriceChange { get; set; }
    
    }
}
