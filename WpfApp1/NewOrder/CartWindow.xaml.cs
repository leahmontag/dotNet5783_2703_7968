using BO;
using DO;
using PL.Products;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        static BlApi.IBl? bl = BlApi.Factory.Get();
        public BO.Cart cart
        {
            get { return (BO.Cart)GetValue(cartProperty); }
            set { SetValue(cartProperty, value); }
        }
        public static readonly DependencyProperty cartProperty =
           DependencyProperty.Register(nameof(cart), typeof(BO.Cart), typeof(CartWindow));
        public string errorProp
        {
            get { return (string)GetValue(errorPropProperty); }
            set { SetValue(errorPropProperty, value); }
        }
        public static readonly DependencyProperty errorPropProperty =
          DependencyProperty.Register(nameof(errorProp), typeof(string), typeof(CartWindow));
        public string expMargin
        {
            get { return (string)GetValue(expMarginProperty); }
            set { SetValue(expMarginProperty, value); }
        }
        public static readonly DependencyProperty expMarginProperty =
          DependencyProperty.Register(nameof(expMargin), typeof(string), typeof(CartWindow));

        public CartWindow(BO.Cart updateingCart)
        {
            cart = updateingCart;
            InitializeComponent();
        }

        private void BtnConfirmOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cart.CustomerEmail == null || !new Regex("[@]").IsMatch(cart.CustomerEmail.ToString()) || !new Regex("[.]").IsMatch(cart.CustomerEmail.ToString()))
                {
                    expMargin = "596,218,0,0";
                    throw new Exception("wrong format of Email");
                }
                else if (cart.CustomerName == null)
                {
                    expMargin = "596,264,0,0";
                    throw new Exception("wrong format of name");
                }
                else if (cart.CustomerAddress == null)
                {
                    expMargin = "596,171,0,0";
                    throw new Exception("wrong format of address");
                }
                BO.Cart c = cart;
                bl.Cart.ConfirmOrder(cart);
                Close();
                new ConfirmOrderWindow(c).Show();
            }
            catch (BO.ProductIsNotAvailableException exp)
            {
                errorProp = exp.Message;
            }
            catch (Exception exp)
            {
                errorProp = exp.Message;
            }


        }
    }
}
