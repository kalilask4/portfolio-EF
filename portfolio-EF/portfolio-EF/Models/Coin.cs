using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio_EF.Models
{
    public class Coin
    {
        [Key]
        public int CoinId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Symbol { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public decimal CurrentPrice { get; set; }
        public decimal ValueUSD { get; set; }
        public decimal AveragePurchasePrice { get; set; }
        public string PictureName { get; set; }
        public DateTime DateUpdate { get; set; }
        // навигационное свойство
        public List<Transaction> transactions { get; set; }

        public Coin(string name, string symbol)
        {
            Name = name;
            Symbol = symbol;
            Amount = 0;
            DateUpdate = DateTime.Now;
            transactions = new List<Transaction>();
        }

        public Coin()
        {
            Name = "Default";
            Symbol = "DEF";
            Amount = 0;
            DateUpdate = DateTime.Now;
            transactions = new List<Transaction>();
        }

    }
}
