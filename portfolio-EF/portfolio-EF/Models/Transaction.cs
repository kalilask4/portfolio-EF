using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio_EF.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        [Required]
        public string Side { get; set; }
        public string TransactionSymbol { get; set; }
        public decimal Amount { get; set; }
        public decimal Priсe { get; set; }
        public DateTime AddDate { get; set; }
        // навигационное свойство
        public List<Coin> transactionCoins { get; set; }

        public Transaction(string side, string transactionSymbol)
        {
            Side = side;
            TransactionSymbol = transactionSymbol;
            AddDate = DateTime.Now;
            transactionCoins = new List<Coin>(2);
        }

        public Transaction(string side, string transactionSymbol, Coin debetCoin, Coin creditCoin)
        {
            Side = side;
            TransactionSymbol = transactionSymbol;
            AddDate = DateTime.Now;
            transactionCoins = new List<Coin>(2);
            transactionCoins.Add(debetCoin);
            transactionCoins.Add(creditCoin);
        }

        public Transaction()
        {
            Side = "DefaultSide";
            TransactionSymbol = "DEFDEF";
            AddDate = DateTime.Now;
            transactionCoins = new List<Coin>(2);
        }
    }
}
