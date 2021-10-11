using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio_EF.Models
{
    public interface ICoinService
    {
        ObservableCollection<Coin> GetAll();
        //void AddTransactionToCoin(EntityContext db, int transactionId, Coin coin);

    }
}

