using portfolio_EF.Models;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace portfolio_EF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EntityContext db;
        ObservableCollection<Coin> coins;
        //ObservableCollection<string> coinDescriptions;

        public MainWindow()
        {
            InitializeComponent();
            db = new EntityContext();
            db.Coins.Load();
            db.Transactions.Load();
            grCoinsData.ItemsSource = db.Coins.Local.ToBindingList();
            TransactionsDataGrid.ItemsSource = db.Transactions.Local.ToBindingList();

            coins = new ObservableCollection<Coin>();
            coins = db.Coins.Local;
      
            cBoxCoin.ItemsSource = db.Coins.Local.ToBindingList();
            getAllCoinsTransactions();
   
            Binding binding = new Binding();

            binding.ElementName = "myTextBox"; // элемент-источник
            binding.Path = new PropertyPath("Text"); // свойство элемента-источника
            myTextBlock.SetBinding(TextBlock.TextProperty, binding); // установка привязки для элемента-приемника
        }
        

        public IQueryable<Transaction> GetTransactions()
        {
            using (db)
            {
                return db.Transactions;
            }
        }


        private void phonesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
                Transaction transaction = new Transaction();

                Coin coin = grCoinsData.SelectedItem as Coin;
                var transactions = db.Transactions.Where(c => c.transactionCoins.Contains(coin)).ToList();
                
                MessageBox.Show(transactions[1].Side);

            
        }

        //all coins and their transactions
        public void getAllCoinsTransactions()
        {
            ListAllCoinsTransaction.ItemsSource = (from coin in db.Coins
                                    from transaction in coin.transactions
                                    select new
                                    {
                                        Coin_name = coin.Name,
                                        Trans_symbol = transaction.TransactionSymbol
                                    }).ToList();
            
        }

        public void transactionsOnCoinToList(object sender, RoutedEventArgs e)
        {
            Coin coin = grCoinsData.SelectedItem as Coin;
            listTransactionsOnCoin.ItemsSource = (from c in db.Coins
                                                     from t in c.transactions
                                                     where c.CoinId == coin.CoinId
                                                     select new
                                                   {
                                                       Coin_name = c.Name,
                                                       Trans_symbol = t.TransactionSymbol
                                                   }).ToList();
        }

        public void getAllCoinsForTransaction(object sender, RoutedEventArgs e)
        {
            Transaction transaction = TransactionsDataGrid.SelectedItem as Transaction;
            ListAllCoinsForTransaction.ItemsSource = (from c in db.Coins
                                                     from t in c.transactions
                                                     where t.TransactionId == transaction.TransactionId
                                                     select new
                                                     {
                                                         Coin_name = c.Name,
                                                         Trans_symbol = t.TransactionSymbol
                                                     }).ToList();
        }


        private void btnAddCoin_Click(object sender, RoutedEventArgs e)
        {
            var coin = new Coin();
            EditCoinWindow editCoinWindow = new EditCoinWindow(coin);
            var result = editCoinWindow.ShowDialog();
            if (result == true)
            {
                db.Coins.Add(coin);
                db.SaveChanges();
                editCoinWindow.Close();
            }

        }

        private void btnDeleteCoin_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure?", "Delete?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Coin coin = grCoinsData.SelectedItem as Coin;
                db.Coins.Remove(coin);
                db.SaveChanges();
            }
        }

        private void btnEditCoin_Click(object sender, RoutedEventArgs e)
        {
            Coin coin = grCoinsData.SelectedItem as Coin;

            EditCoinWindow editCoinWindow = new EditCoinWindow(coin);
            var result = editCoinWindow.ShowDialog();
            if (result == true)
            {
                db.SaveChanges();
                editCoinWindow.Close();
            }
            else
            {
       
                db.Entry(coin).Reload();
          
                grCoinsData.DataContext = null;
                grCoinsData.DataContext = db.Coins.Local;
            }
        }

        private void btnAddTransaction_Click(object sender, RoutedEventArgs e)
        {
            var transaction = new Transaction();
            EditTransactionWindow editTransactionWindow = new EditTransactionWindow(transaction);
            var result = editTransactionWindow.ShowDialog();
            if (result == true)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                editTransactionWindow.Close();
            }


        }

        private void btnDeleteTransaction_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure?", "Delete?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Transaction transaction = TransactionsDataGrid.SelectedItem as Transaction;
                db.Transactions.Remove(transaction);
                db.SaveChanges();
            }
                
        }

        private void btnUpdateAllCoinsTransaction_Click(object sender, RoutedEventArgs e)
        {

            getAllCoinsTransactions();
        }

        
    }
}
