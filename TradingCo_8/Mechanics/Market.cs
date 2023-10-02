using Microsoft.Maui.Dispatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingCo.Mechanics
{
    public class Market
    {
        private MaterialStorage MaterialStorage { get; set; }
        private Thread MarketPricingThread { get; set; }

        public bool needRefresh = false;
        public bool pauseMarket = false;

        public Market(MaterialStorage materialStorage) {
            MaterialStorage = materialStorage;
            MarketPricingThread = new Thread(MarketPricingLoop);
            MarketPricingThread.Start();
        }

        public void MarketPricingLoop() {

            while (true) {
                Thread.Sleep(5000);

                if (pauseMarket) { continue; }

                var rand = new Random();

                foreach (var mat in MaterialStorage.MaterialStorageList) {
                    var currentPrice = mat.Price;

                    double precent = rand.Next(0, 45);
                    precent = precent / 100;

                    double change = precent * currentPrice;

                    var sign = rand.Next(1, 7);
                    if ((sign <= 3 || currentPrice > GetMaterialMax(mat.BasePrice))
                        && currentPrice > GetMaterialMin(mat.BasePrice) ) {

                        change = change * -1;
                    }
                    if (currentPrice < GetMaterialMin(mat.BasePrice)) {
                        change = change + currentPrice / 3;
                    }
                    var newPrice = currentPrice + change;
                    mat.Price = newPrice;
                }

            }

        }

        public void AdjustMaterialPrice(Material mat, double change) {
            var currentPrice = mat.Price;
            
        
        }

        public void AdjustMaterialBasePrice(Material mat, double change) {
            var currentPrice = mat.BasePrice;


        }

        private double GetMaterialMax(double basePrice) {
            double maxPrice = (basePrice * 1.75);
            return maxPrice;
        }
        private double GetMaterialMin(double basePrice) {
            double minPrice = (basePrice * 0.50);
            return minPrice;
        }




    }
}
