using BO;
using PL.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL.NewOrder
{
    /// <summary>
    /// Interaction logic for NewOrderWindow.xaml
    /// </summary>
    public partial class NewOrderWindow : Window
    {
        static BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart cart = new() ;
        public IEnumerable<BO.ProductItem?> productItem
        {
            get { return (IEnumerable<BO.ProductItem?>)GetValue(productItemsProperty); }
            set { SetValue(productItemsProperty, value); }
        }
        public static readonly DependencyProperty productItemsProperty =
           DependencyProperty.Register(nameof(productItem), typeof(IEnumerable<BO.ProductItem?>), typeof(NewOrderWindow));

        public BO.Enums.Category? selectedCategory
        {
            get { return (BO.Enums.Category?)GetValue(selectedCategoryProperty); }
            set { SetValue(selectedCategoryProperty, value); }
        }
        public static readonly DependencyProperty selectedCategoryProperty =
           DependencyProperty.Register(nameof(selectedCategoryProperty), typeof(BO.Enums.Category?), typeof(NewOrderWindow));


        public System.Array categories { get; set; } = Enum.GetValues(typeof(BO.Enums.Category));
        public ProductItem selectedProduct { get; set; } = new();



        BO.Cart cartBL = new();
        public NewOrderWindow()
        {
            selectedCategory = null;
            productItem = bl.Product.GetAllProductsItemFromCatalog(cart);
            InitializeComponent();

        }
        public NewOrderWindow(BO.Cart updatingCart)
        {
            selectedCategory = null;
            productItem = bl.Product.GetAllProductsItemFromCatalog(updatingCart);
            cart=updatingCart;
            InitializeComponent();

        }
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            productItem = bl.Product.GetAllProductsItemFromCatalog(cart, x => x?.Category.ToString() == selectedCategory.ToString());
        }
        private void ProductItemView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Close();
            new SingleProductItemWindow(cart,selectedProduct.ID).Show();
        }
        private void BtnMoveToCart_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new CartWindow(cart).Show();
        }
        private void BtnGroupByCategory_Click(object sender, RoutedEventArgs e)
        {
            selectedCategory = null;
            var w = bl.Product.GetAllProductsItemGroupByCategory(cart);
            MessageBox.Show(w.ToString());
        }

        

    }
}
