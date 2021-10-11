using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio_EF.Models
{
    class DataBaseInitializer : DropCreateDatabaseIfModelChanges<EntityContext>
    {
        protected override void Seed(EntityContext context) {
            Random r = new Random();
            var coins = new List<Coin>();
            var transactions = new List<Transaction>();

            for (var i = 0; i < 10; i++)
            {
                var coin = new Coin($"Coin {i}", $"C0{i}");
                coins.Add(coin);
                //Trace.WriteLine($"Test setup coin in \"for\": {coin}");
            }

            for (var i = 0; i < 5; i++)
            {
                var transaction = new Transaction("BUY", $"TR{i}");
                transactions.Add(transaction);
            }

            //Добавить транш
            transactions.Add(new Transaction("Coin3", coins[3]));

            //Привязка монет и транзакций
            coins[0].transactions.Add(transactions[0]);
            coins[0].transactions.Add(transactions[1]);
            transactions[0].transactionCoins.Add(coins[0]);
            transactions[0].transactionCoins.Add(coins[1]);
            transactions[1].transactionCoins.Add(coins[0]);
            Trace.WriteLine("Test setup coin " + transactions[0].transactionCoins[0].ToString());

            context.Coins.AddRange(coins);
            context.Transactions.AddRange(transactions);


            //    context.Coins.AddRange(

            //        new Coin[]
            //{
            //    coins[0] = new Coin{
            //        Name = "CoinName0",
            //        Symbol = "C0"
            //    },
            //    coins[1] = new Coin{
            //        Name = "CoinName1",
            //        Symbol = "C1"
            //    },
            //    coins[2] = new Coin{
            //        Name = "CoinName2",
            //        Symbol = "C2"
            //    }
            //coins[3] = new Coin{});
      
            /*
            context.Transactions.AddRange(new Transaction[]
            {
                transactions[0] = new Transaction
                {
                    Side = "BUY",
                    TransactionSymbol = "TRB0"
                },
                transactions[1] = new Transaction
                {
                    Side = "SELL",
                    TransactionSymbol = "TRB2"
                },
                transactions[2] = new Transaction
                {
                    Side = "BUY",
                    TransactionSymbol = "TRB3",
                }
            });

            context.Coins.Find(0).transactions.Add(context.Transactions.Find(0));
            context.Coins.Find(1).transactions.Add(context.Transactions.Find(0));
            context.Transactions.Find(0).transactionCoins.Add(context.Coins.Find(0));
            context.Transactions.Find(0).transactionCoins.Add(context.Coins.Find(1));
            */
        }
    }

}
