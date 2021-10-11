using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio_EF.Models
{
    public class CoinDTO0
    {
        public int CoinId { get; set; }
        public string Name { get; set; }
        public ObservableCollection<TransactionDTO0> Transaction
        { get; set; }

        public CoinDTO0()
        {
            Name = "coin name";
        }
    }
}

