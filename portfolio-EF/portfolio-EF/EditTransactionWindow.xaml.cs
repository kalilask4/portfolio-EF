﻿using portfolio_EF.Models;
using System;
using System.Collections.Generic;
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
        Transaction transaction;

        public EditTransactionWindow(Transaction transaction)
        {
            InitializeComponent();
            this.transaction = transaction;
            grid.DataContext = transaction;
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
