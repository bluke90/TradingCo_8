using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCo.Mechanics;

namespace TradingCo.Data
{
    public class MaterialContext : DbContext
    {
        public DbSet<Material> Materials { get; set; }
        public DbSet<Balance> Balances { get; set; } 
        public DbSet<Transaction> Transactions { get; set; }
        public MaterialContext() {
            SQLitePCL.Batteries_V2.Init();

            this.Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "dataContext.db3");

            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }


    }
}
