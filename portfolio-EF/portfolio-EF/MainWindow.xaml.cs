using portfolio_EF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace portfolio_EF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static EntityContext db;
        ObservableCollection<Coin> coins;
        //ObservableCollection<string> coinDescriptions;

        public MainWindow()
        {
            InitializeComponent();
            db = new EntityContext();
            db.Coins.Load();
            db.Transactions.Load();
            grCoinsData.ItemsSource = db.Coins.Local.ToBindingList();
            grTransactionsData.ItemsSource = db.Transactions.Local.ToBindingList();

            coins = new ObservableCollection<Coin>();
            coins = db.Coins.Local;
            allRelationsCoinsTransactions();
            
            
            cBoxCoin.ItemsSource = db.Coins.Local.ToBindingList();
            cBoxTransaction.ItemsSource = db.Transactions.Local.ToBindingList();
            tbDeskCoin.Text = cBoxCoin.SelectedItem?.ToString();
            tbDeskTransaction.Text = cBoxTransaction.SelectedItem?.ToString();

            Binding binding = new Binding();

            binding.ElementName = "myTextBox"; // элемент-источник
            binding.Path = new PropertyPath("Text"); // свойство элемента-источника
            myTextBlock.SetBinding(TextBlock.TextProperty, binding); // установка привязки для элемента-приемника

        }

        private void cBoxTransaction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Transaction)(sender as ComboBox).SelectedItem;
            string text = selectedItem?.ToString();
            tbDeskTransaction.Text = text;
        }       
        
        private void cBoxCoin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Disclaims the penultimate selection.
            //It's weird that SelectedItem holds the fresh data, whereas SelectedValue doesn't
            //string text = cBoxCoin.Text;
            //TextDeskCoin.Text = text;
            var selectedItem = (Coin)(sender as ComboBox).SelectedItem;
            string text = selectedItem?.ToString();
            tbDeskCoin.Text = text;
        }
        

        public IQueryable<Transaction> GetTransactions()
        {
            using (db)
            {
                return db.Transactions;
            }
        }

        //all relations coins-transactions
        public void allRelationsCoinsTransactions()
        {
            listAllRelationsCoinsTransaction.ItemsSource = (from coin in db.Coins
                                    from transaction in coin.transactions
                                    select new
                                    {
                                        coin.CoinId,
                                        name = coin.Name,
                                        transaction.TransactionId,
                                        symbol = transaction.TransactionSymbol
                                    }).ToList();
            
        }

        public void transactionsOnCoinToList(object sender, RoutedEventArgs e)
        {
            Coin coin = grCoinsData.SelectedItem as Coin;
            try
            {
                listTransactionsOnCoin.ItemsSource = (from c in db.Coins
                                                      from t in c.transactions
                                                      where c.CoinId == coin.CoinId
                                                      select new
                                                      {
                                                          Coin_name = c.Name,
                                                          Trans_symbol = t.TransactionSymbol
                                                      }).ToList();
            }
            catch { }
        }

        public void coinsOnTransactionToList(object sender, RoutedEventArgs e)
        {
            Transaction transaction = grTransactionsData.SelectedItem as Transaction;
            try
            {
                ListAllCoinsForTransaction.ItemsSource = (from c in db.Coins
                                                          from t in c.transactions
                                                          where t.TransactionId == transaction.TransactionId
                                                          select new
                                                          {
                                                              Coin_name = c.Name,
                                                              Trans_symbol = t.TransactionSymbol
                                                          }).ToList();
            }
            catch { }
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
            var coinDebet = new Coin();
            var coinCredit = new Coin();
            EditTransactionWindow editTransactionWindow = new EditTransactionWindow(transaction, coinDebet , coinCredit/*, db*/);
            var result = editTransactionWindow.ShowDialog();
            if (result == true)
            {
                transaction.transactionCoins.Add(coinDebet);
                transaction.transactionCoins.Add(coinCredit);
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
                Transaction transaction = grTransactionsData.SelectedItem as Transaction;
                db.Transactions.Remove(transaction);
                db.SaveChanges();
            }
        }

        private void btnUpdateAllRelationsCoinsTransaction_Click(object sender, RoutedEventArgs e)
        {
            allRelationsCoinsTransactions();
        }
    }
}
