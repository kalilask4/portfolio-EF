using portfolio_EF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

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
            CoinsDataGrid.ItemsSource = db.Coins.Local.ToBindingList();
            TransactionsGrid.ItemsSource = db.Transactions.Local.ToBindingList();


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

                Coin coin = CoinsDataGrid.SelectedItem as Coin;
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

        public void getAllTransactionsForCoin(object sender, RoutedEventArgs e)
        {
            ListAllTransactionForCoin.ItemsSource = (from coin in db.Coins
                                                   from transaction in coin.transactions
                                                   select new
                                                   {
                                                       Coin_name = coin.Name,
                                                       Trans_symbol = transaction.TransactionSymbol
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
                Coin coin = CoinsDataGrid.SelectedItem as Coin;
                db.Coins.Remove(coin);
                db.SaveChanges();
            }
        }

        private void btnEditCoin_Click(object sender, RoutedEventArgs e)
        {
            Coin coin = CoinsDataGrid.SelectedItem as Coin;

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
          
                CoinsDataGrid.DataContext = null;
                CoinsDataGrid.DataContext = db.Coins.Local;
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
                Transaction transaction = TransactionsGrid.SelectedItem as Transaction;
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
