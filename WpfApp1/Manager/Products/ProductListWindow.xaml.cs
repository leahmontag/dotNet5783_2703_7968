using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        private ObservableCollection<BO.ProductForList?> _myCollection;
        public ProductListWindow()
        {
            InitializeComponent();
            _myCollection = new(bl.Product.GetAll());
            ProductsListView.DataContext = _myCollection;
            CategorySelector.DataContext = Enum.GetValues(typeof(BO.Enums.Category));

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string? selectedItem = CategorySelector.SelectedItem.ToString();
            _myCollection = new(bl.Product.GetAll(x => x?.Category.ToString() == selectedItem));
            ProductsListView.DataContext = _myCollection;
        }

        private void addNewProductButton_Click(object sender, RoutedEventArgs e)
        {
            string Btn = "Add";
            this.Close();
            new ProductWindow(Btn).Show();
        }

        private void ProductsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList SelectedProduct = (BO.ProductForList)((sender as ListView).SelectedItem);
            string Btn = "Update";
            this.Close();
            new ProductWindow(Btn, SelectedProduct.ID).Show();
        }

    }
}
