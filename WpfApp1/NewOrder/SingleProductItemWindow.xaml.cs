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

        public BO.Cart cart { get; set; }=new();

        public SingleProductItemWindow(BO.Cart cartFromCatalog,int selectedProductId)
        {
            cart= cartFromCatalog;
            product = bl.Product.GetProductFromCatalog(cart, x => x?.ID.ToString() == selectedProductId.ToString());
            VisibileRemoveItemFromCart = true;
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            VisibileRemoveItemFromCart = false;
            cart = bl.Cart.Create(cart, product.ID);
            product = bl.Product.GetProductFromCatalog(cart, x => x?.ID.ToString() == product.ID.ToString());
        }

        private void BtnDecrease_Click(object sender, RoutedEventArgs e)
        {
            cart=bl.Cart.Update(cart, product.ID,product.Amount-1);
            product = bl.Product.GetProductFromCatalog(cart, x => x?.ID.ToString() == product.ID.ToString());
            if(product.Amount==0)
                VisibileRemoveItemFromCart = true;
        }

        private void BtnBackToCatalog_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new NewOrderWindow(cart).Show();
        }

        private void BtnMoveToCart_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new CartWindow(cart).Show();
        }
    }
}
