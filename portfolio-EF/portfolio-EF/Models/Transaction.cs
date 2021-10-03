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
        public List<Coin> transactionCoin;

    }
}
