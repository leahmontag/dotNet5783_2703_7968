using BO;
using PL.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for SingleProductItemWindow.xaml
    /// </summary>
    public partial class SingleProductItemWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        //BO.Cart cart = new();
        public ObservableCollection<BO.ProductItem?> productItem
        {
            get { return (ObservableCollection<BO.ProductItem?>)GetValue(productItemsProperty); }
            set { SetValue(productItemsProperty, value); }
        }
        public static readonly DependencyProperty productItemsProperty =
           DependencyProperty.Register(nameof(productItem), typeof(ObservableCollection<BO.ProductItem?>), typeof(SingleProductItemWindow));

        public BO.ProductItem product
        {
            get { return (BO.ProductItem)GetValue(productProperty); }
            set { SetValue(productProperty, value); }
        }
        public static readonly DependencyProperty productProperty =
           DependencyProperty.Register(nameof(product), typeof(BO.ProductItem), typeof(SingleProductItemWindow));
        public bool VisibileRemoveItemFromCart
        {
            get { return (bool)GetValue(VisibileRemoveItemFromCartProperty); }
            set { SetValue(VisibileRemoveItemFromCartProperty, value); }
        }
        public static readonly DependencyProperty VisibileRemoveItemFromCartProperty =
           DependencyProperty.Register(nameof(VisibileRemoveItemFromCart), typeof(bool), typeof(SingleProductItemWindow));
        public string errorProp
        {
            get { return (string)GetValue(errorPropProperty); }
            set { SetValue(errorPropProperty, value); }
        }
        public static readonly DependencyProperty errorPropProperty =
          DependencyProperty.Register(nameof(errorProp), typeof(string), typeof(SingleProductItemWindow));

        public int textBox
        {
            get { return (int)GetValue(textBoxPropProperty); }
            set { SetValue(textBoxPropProperty, value); }
        }
        public static readonly DependencyProperty textBoxPropProperty =
          DependencyProperty.Register(nameof(textBox), typeof(int), typeof(SingleProductItemWindow));
        public BO.Cart cart { get; set; } = new();

        private Action<ProductItem?> action;
        public SingleProductItemWindow(BO.Cart cartFromCatalog, int selectedProductId, Action<ProductItem?> action)
        {
            cart = cartFromCatalog;
            product = bl.Product.GetProductFromCatalog(cart, x => x?.ID.ToString() == selectedProductId.ToString());
            productItem = new ObservableCollection<BO.ProductItem?>(bl.Product.GetAllProductsItemFromCatalog(cart).Cast<BO.ProductItem?>());
            if (product.Amount == 0)
                VisibileRemoveItemFromCart = true;
            InitializeComponent();
            this.action = action;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                errorProp = "";
                if (textBox != 0)
                    VisibileRemoveItemFromCart = false;
                if (product.Amount == 0)
                    cart = bl.Cart.Create(cart, product.ID);
                cart = bl.Cart.Update(cart, product.ID,textBox);
                actionFunction();
            }
            catch (BO.FailedToDisplayAllItemsException exp)
            {
                errorProp = exp.Message;
                VisibileRemoveItemFromCart = true;
            }
            catch (BO.ProductIsNotAvailableException exp)
            {
                errorProp = exp.Message;
                VisibileRemoveItemFromCart = true;
            }
            textBox = 0;
        }

        private void BtnDecrease_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                errorProp = "";
                cart = bl.Cart.Update(cart, product.ID, 0);
                actionFunction();
                VisibileRemoveItemFromCart = true;
            }
            catch (BO.FailedToDisplayAllItemsException exp)
            {
                errorProp = exp.Message;
            }
            catch (BO.ProductIsNotAvailableException exp)
            {
                errorProp = exp.Message;
            }
            textBox = 0;
        }

        private void BtnBackToCatalog_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnMoveToCart_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new CartWindow(cart, updateingTheCatalog).Show();
        }
        private void updateingTheCatalog(ProductItem? productUpdate)
        {
            var item = productItem.FirstOrDefault(item => item?.ID == productUpdate?.ID);
            if (item != null)
                productItem[productItem.IndexOf(item)] = productUpdate;
            actionFunction(productUpdate);
        }
        private void actionFunction(ProductItem? productUpdate)
        {
            //product = bl.Product.GetProductFromCatalog(cart, x => x?.ID.ToString() == productUpdate.ID.ToString());
            //action(new ProductItem
            //{
            //    ID = product.ID,
            //    Amount = product.Amount,
            //    Name = product.Name,
            //    Price = product.Price,
            //    InStock = product.InStock,
            //    Category = product.Category
            //});
            action(productUpdate);
        }
        private void actionFunction()
        {
            product = bl.Product.GetProductFromCatalog(cart, x => x?.ID.ToString() == product.ID.ToString());
            action(new ProductItem
            {
                ID = product.ID,
                Amount = product.Amount,
                Name = product.Name,
                Price = product.Price,
                InStock = product.InStock,
                Category = product.Category
            });
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
