using portfolio_EF.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace portfolio_EF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EntityContext db;

        public MainWindow()
        {
            InitializeComponent();
            db = new EntityContext();

            db.Coins.Load();
            CoinsGrid.ItemsSource = db.Coins.Local.ToBindingList(); 
            
            db.Transactions.Load();
            TransactionsGrid.ItemsSource = db.Transactions.Local.ToBindingList();

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
                Coin coin = CoinsGrid.SelectedItem as Coin;
                db.Coins.Remove(coin);
                db.SaveChanges();
            }
        }

        private void btnEditCoin_Click(object sender, RoutedEventArgs e)
        {
            Coin coin = CoinsGrid.SelectedItem as Coin;

            EditCoinWindow editCoinWindow = new EditCoinWindow(coin);
            var result = editCoinWindow.ShowDialog();
            if (result == true)
            {
                db.SaveChanges();
                editCoinWindow.Close();
            }
            else
            {
                //return start value
                db.Entry(coin).Reload();
                //reload DataConext
                CoinsGrid.DataContext = null;
                CoinsGrid.DataContext = db.Coins.Local;
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
    }
}
