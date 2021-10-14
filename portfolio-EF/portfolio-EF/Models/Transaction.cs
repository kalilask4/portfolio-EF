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
        string side;
        public static List<string> sideType = new List<string> { "buy", "sell", "transfer" };
        //public List<string> SideType {get; }

        public string Side
        {
            get { return side; }
            set
            {
                if (sideType.Contains(value))
                    side = value;
            }
        }
        


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

        public Transaction(string transactionSymbol, Coin debetCoin)
        {
            Side = "transfer";
            TransactionSymbol = transactionSymbol;
            AddDate = DateTime.Now;
            transactionCoins = new List<Coin>(2);
            transactionCoins.Add(debetCoin);
        }

        public Transaction()
        {
            Side = "DefaultSide";
            TransactionSymbol = "DEFDEF";
            AddDate = DateTime.Now;
            transactionCoins = new List<Coin>(2);
        }

        public override string ToString()
        {
            return $"Id {TransactionId} {TransactionSymbol} {Side}";
        }


    }
}
