using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio_EF.Models
{
    class DataBaseInitializer : DropCreateDatabaseIfModelChanges<EntityContext>
    {
        protected override void Seed(EntityContext context) {
            context.Coins.AddRange(new Coin[]
            {
                new Coin{
                    Name = "CoinName0",
                    Symbol = "C0"
                },
                new Coin{
                    Name = "CoinName1",
                    Symbol = "C1"
                },
                new Coin{
                    Name = "CoinName2",
                    Symbol = "C2"
                },
                new Coin{},
            });
            context.Transactions.AddRange(new Transaction[]
            {
                new Transaction
                {
                    Side = "BUY",
                    TransactionSymbol = "TRB0"
                },
                new Transaction
                {
                    Side = "SELL",
                    TransactionSymbol = "TRB2"
                },
                new Transaction
                {
                    Side = "BUY",
                    TransactionSymbol = "TRB3",
                }
            });
        }
    }

}
