﻿using portfolio_EF.Models;
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
    }
}
