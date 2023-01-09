using BO;
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

        public CartWindow(BO.Cart updateingCart)
        {
            cart= updateingCart;
            InitializeComponent();
        }

        private void BtnConfirmOrder_Click(object sender, RoutedEventArgs e)
        {
            BO.Cart c = cart;
            bl.Cart.ConfirmOrder(cart);
           Close();
            new ConfirmOrderWindow(c).Show();
        }
    }
}
