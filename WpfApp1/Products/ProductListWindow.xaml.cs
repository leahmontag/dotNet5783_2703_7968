﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace PL.Products
{
    /// <summary>
    /// Interaction logic for BoProductListWindow.xaml
    /// </summary>
    public partial class BoProductListWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public BoProductListWindow()
        {
            InitializeComponent();
            ProductsListView.ItemsSource = bl.Product.GetAll();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedItem = CategorySelector.SelectedItem.ToString();
            ProductsListView.ItemsSource = bl.Product.GetAll(x => x?.Category.ToString() == selectedItem);
        }

        private void addNewProductButton_Click(object sender, RoutedEventArgs e)
        {
            string Btn = "Add";
            this.Close();
            new BoProductWindow(Btn).Show();
        }

        private void ProductsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList SelectedProduct = (BO.ProductForList)((sender as ListView).SelectedItem);
            string Btn = "Update";
            this.Close();
            new BoProductWindow(Btn, SelectedProduct.ID).Show();
        }

    }
}
