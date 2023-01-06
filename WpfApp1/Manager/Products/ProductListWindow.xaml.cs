using BO;
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
        static BlApi.IBl? bl = BlApi.Factory.Get();
        public IEnumerable<BO.ProductForList?> productForList
        {
            get { return (IEnumerable<BO.ProductForList?>)GetValue(productForListsProperty); }
            set { SetValue(productForListsProperty, value); }
        }
        public static readonly DependencyProperty productForListsProperty =
           DependencyProperty.Register(nameof(productForList), typeof(IEnumerable<BO.ProductForList?>), typeof(ProductListWindow));

        public System.Array categories { get; set; } = Enum.GetValues(typeof(DO.Enums.Category));
        public DO.Enums.Category selectedCategory { get; set; }
        public ProductForList selectedProduct { get; set; } = new();

        public ProductListWindow()
        {
            InitializeComponent();
            productForList = bl.Product.GetAll();
        }


        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            productForList = bl.Product.GetAll(x => x?.Category.ToString() == selectedCategory.ToString());
        }

        private void addNewProductButton_Click(object sender, RoutedEventArgs e)
        {
            string Btn = "Add";
            this.Close();
            new ProductWindow(Btn).Show();
        }

        private void ProductsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string Btn = "Update";
            this.Close();
            new ProductWindow(Btn, selectedProduct.ID).Show();
        }

    }
}
