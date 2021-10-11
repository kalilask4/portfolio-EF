using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio_EF.Models
{
    public class TransactionDTO0
    {
        public int TransactionId { get; set; }
        public string Side { get; set; }
        public TransactionDTO0()
        {
            Side = "side";
        }
    }
}