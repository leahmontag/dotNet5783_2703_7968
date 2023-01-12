using BO;
using PL.Products;
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

namespace PL.NewOrder
{
    /// <summary>
    /// Interaction logic for SingleProductItemWindow.xaml
    /// </summary>
    public partial class SingleProductItemWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        //BO.Cart cart = new();
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
        public BO.Cart cart { get; set; }=new();

        private Action<ProductItem?> action;
        public SingleProductItemWindow(BO.Cart cartFromCatalog,int selectedProductId, Action<ProductItem?> action)
        {
            cart= cartFromCatalog;
            product = bl.Product.GetProductFromCatalog(cart, x => x?.ID.ToString() == selectedProductId.ToString());
            if (product.Amount == 0)
                VisibileRemoveItemFromCart = true;
            InitializeComponent();
            this.action = action;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VisibileRemoveItemFromCart = false;
                cart = bl.Cart.Create(cart, product.ID);
                product = bl.Product.GetProductFromCatalog(cart, x => x?.ID.ToString() == product.ID.ToString());
            }
            catch (BO.FailedToDisplayAllItemsException exp)
            {
                errorProp = exp.Message;
            }
        }

        private void BtnDecrease_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cart = bl.Cart.Update(cart, product.ID, product.Amount - 1);
                product = bl.Product.GetProductFromCatalog(cart, x => x?.ID.ToString() == product.ID.ToString());
                if (product.Amount == 0)
                    VisibileRemoveItemFromCart = true;
            }
            catch (BO.FailedToDisplayAllItemsException exp)
            {
                errorProp = exp.Message;
            }

        }

        private void BtnBackToCatalog_Click(object sender, RoutedEventArgs e)
        {
            action(new ProductItem
            {
                ID =product.ID,
                Amount=product.Amount,
                Name=product.Name,
                Price=product.Price,
                InStock=product.InStock,
                Category=product.Category
            });
            Close();
        }

        private void BtnMoveToCart_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new CartWindow(cart).Show();
        }
    }
}
