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
        public ObservableCollection<BO.ProductItem?> productItemTemp;
        BO.Cart cart = new();
        public ObservableCollection<BO.ProductItem?> productItem
        {
            get { return (ObservableCollection<BO.ProductItem?>)GetValue(productItemsProperty); }
            set { SetValue(productItemsProperty, value); }
        }
        public static readonly DependencyProperty productItemsProperty =
           DependencyProperty.Register(nameof(productItem), typeof(ObservableCollection<BO.ProductItem?>), typeof(NewOrderWindow));

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
            productItem = new ObservableCollection<BO.ProductItem?>(bl.Product.GetAllProductsItemFromCatalog(cart).Cast<BO.ProductItem?>());
            productItemTemp = productItem;
            InitializeComponent();

        }
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            productItem = productItemTemp;
            var GropupingProducts = (from p in productItem
                                     where p.Category == selectedCategory
                                     group p by p.Category into catGroup
                                     from pr in catGroup
                                     select pr).ToList();
            productItem = new(GropupingProducts);
        }
        private void ProductItemView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new SingleProductItemWindow(cart, selectedProduct.ID, updateTheCatalog).Show();
        }
        private void BtnMoveToCart_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new CartWindow(cart, updateTheCatalog).Show();
        }
        private void BtnGroupByCategory_Click(object sender, RoutedEventArgs e)
        {
            selectedCategory = null;
            var GropupingProducts = (from p in productItem
                                     group p by p.Category into catGroup
                                     from pr in catGroup
                                     select pr).ToList();

            productItem = new(GropupingProducts);
        }
        public void updateTheCatalog(ProductItem? productUpdate)
        {
            var item = productItem.FirstOrDefault(item => item?.ID == productUpdate?.ID);
            if (item != null)
                productItem[productItem.IndexOf(item)] = productUpdate;
            productItemTemp = productItem;
        }

    }
}
