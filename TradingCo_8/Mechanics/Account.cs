using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCo.Data;

namespace TradingCo.Mechanics
{
    public class Account {
        private Balance _balance { get; set; }
        public AccountPage AccountPage { get;set; }
        private MaterialContext _db { get; set; }

        public Account(MaterialContext context) {
            _db = context;
            GetBalance();
        }   

        private void GetBalance() {
            _balance = _db.Balances.FirstOrDefault();
            if (_balance == null) { 
                _balance = new Balance();
                _db.Balances.Add(_balance);
                _db.SaveChanges();
            }

        }

        /// <summary>
        /// Get Account Transactions from storage and populate them on the accountPage
        /// </summary>
        public async void GetTransactions() {
            foreach (var t in await _db.Transactions.ToListAsync()) {
                AccountPage.PopulateTransaction(t);
            }
        }

        private void SaveDbChanges() {
            _db.SaveChanges();
        }

        public double GetCurrentBalance() { return _balance.CurrentValue;  }

        public double GetLastBalance() { return _balance.LastValue; }

        public async void BuyItem(double currentPrice, int amount) {
            var current = _balance.CurrentValue;

            var total = currentPrice * amount;

            _balance.CurrentValue = current - total;
            _balance.LastValue = current;

            SaveDbChanges();

            var tansaction = new Transaction()
            {
                PreBalance = _balance.LastValue,
                PostBalance = _balance.CurrentValue,
                Amount = total*-1,
                Quantity = amount
            };
            _db.Transactions.Add(tansaction);
            await _db.SaveChangesAsync();
            //AccountPage.PopulateTransaction(tansaction);
            AccountPage.UpdateAccountPage();
        }

        public void SellItem(double currentPrice, int amount) {
            var current = _balance.CurrentValue;

            var total = currentPrice * amount;

            SaveDbChanges();

            _balance.CurrentValue = current + total;
            _balance.LastValue = current;

            var tansaction = new Transaction()
            {
                PreBalance = _balance.LastValue,
                PostBalance = _balance.CurrentValue,
                Amount = total ,
                Quantity = amount
            };
            _db.Transactions.Add(tansaction);
            SaveDbChanges();
            AccountPage.PopulateTransaction(tansaction);
        }

    }

    public class Balance {

        public Balance() {
            CurrentValue = 1500;
            LastValue = CurrentValue;
        }

        public int Id { get; set; }
        public double CurrentValue { get; set; }
        public double LastValue { get; set; }
    }
}
