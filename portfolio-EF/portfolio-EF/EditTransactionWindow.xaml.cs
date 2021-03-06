using portfolio_EF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace portfolio_EF
{
    /// <summary>
    /// Логика взаимодействия для EditTransactionWindow.xaml
    /// </summary>
    public partial class EditTransactionWindow : Window
    {
        //Transaction transaction;
        //Coin coinDebet;
        //Coin coinCredit;

        public EditTransactionWindow(Transaction transaction, Coin coinDebet , Coin coinCredit/*, EntityContext db*/)
        {
            InitializeComponent();
            //this.transaction = transaction;
            grid.DataContext = transaction;

            cBoxCoinDebet.ItemsSource = MainWindow.db.Coins.Local.ToBindingList();
            cBoxCoinCredit.ItemsSource = MainWindow.db.Coins.Local.ToBindingList();
            cBoxSide.ItemsSource = Transaction.sideType;
            //cBoxSide.SelectedIndex = 0; /doesn't work
            //textBoxSum.Content = 0;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cBoxCoinDebet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Disclaims the penultimate selection.
            //It's weird that SelectedItem holds the fresh data, whereas SelectedValue doesn't
            //string text = cBoxCoin.Text;
            //TextDeskCoin.Text = text;
            var selectedItem = (Coin)(sender as ComboBox).SelectedItem;
            string text = selectedItem?.ToString();
            tbDeskCoinDebet.Text = text;
        }
        private void cBoxCoinCredit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Disclaims the penultimate selection.
            //It's weird that SelectedItem holds the fresh data, whereas SelectedValue doesn't
            //string text = cBoxCoin.Text;
            //TextDeskCoin.Text = text;
            var selectedItem = (Coin)(sender as ComboBox)?.SelectedItem;
            string text = selectedItem?.ToString();
            tbDeskCoinCredit.Text = text;
        }

        private void btnCulculateSum_Click(object sender, RoutedEventArgs e)
        {
            decimal v1 = Convert.ToDecimal(textBoxAmount.Text);
            decimal v2 = Convert.ToDecimal(textBoxPrice.Text);
            textBoxSum.Content = v1*v2;
        }

        private void btnFill_Click(object sender, RoutedEventArgs e)
        {
            decimal v1 = Convert.ToDecimal(textBoxAmount.Text);
            decimal v2 = Convert.ToDecimal(textBoxPrice.Text);
            textBoxSum.Content = v1 * v2;
            Coin coinDebet = cBoxCoinDebet.SelectedItem as Coin;
            Coin coinCredit = cBoxCoinCredit.SelectedItem as Coin;
            textBoxSymbol.Text = coinDebet.Symbol + coinCredit?.Symbol;
        }
    }
}
