using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCo.Data;

namespace TradingCo.Mechanics
{
    public class MaterialStorage
    {
        private static List<Material> MaterialList = new List<Material>() {
            {new Material(){ Name = "Wood", BasePrice = 100, Price = 100, Quantity = 0, IconName = "wood.png"} },
            {new Material(){ Name = "Copper", BasePrice = 450, Price = 450, Quantity = 0, IconName = "copper.png"} },
            {new Material(){ Name = "Silver", BasePrice = 600, Price = 600, Quantity = 0, IconName = "silver.png"} },
            {new Material(){ Name = "Gold", BasePrice = 850, Price = 850, Quantity = 0, IconName = "gold.png"} }
        };

        public List<Material> MaterialStorageList { get; set; }

        private MaterialContext _db { get; set; }

        public MaterialStorage(MaterialContext db) {
            _db = db;            
            MaterialStorageList = new List<Material>();
            var count = _db.Materials.Count();
            
            // check if materials are in material list
            if (_db.Materials.Count() > 1) {    // if populated - get list 
                
                var getTask = GetMaterialStorageList();
                getTask.Wait();

            } else {    // else - create materialStorageList

                foreach (var mat in MaterialList) {
                    MaterialStorageList.Add(mat);
                    _db.Materials.Add(mat);
                }
                SaveMaterialStorageList();
            
            }
        }

        public async Task GetMaterialStorageList() {

            foreach (var mat in await _db.Materials.ToListAsync()) {
                MaterialStorageList.Add(mat);
            }

        }

        public void SaveMaterialStorageList() {

            foreach (var mat in MaterialStorageList) {
                var dbMat = _db.Materials.FirstOrDefault(m => m.Name == mat.Name);
                dbMat = mat;
            }

            _db.SaveChanges();
            Console.WriteLine($"Saving change to material list => Qty: {_db.Materials.Count()}");
        }


    }
}
