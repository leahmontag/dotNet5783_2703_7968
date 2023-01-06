using BO;
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

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderItemsWindow.xaml
    /// </summary>
    public partial class OrderItemsWindow : Window
    {

        BlApi.IBl? bl = BlApi.Factory.Get();
        public IEnumerable<BO.OrderItem?> orderItemsForList
        {
            get { return (IEnumerable<BO.OrderItem?>)GetValue(orderItemProperty); }
            set { SetValue(orderItemProperty, value); }
        }
        public static readonly DependencyProperty orderItemProperty =
           DependencyProperty.Register(nameof(orderItemsForList), typeof(IEnumerable<BO.OrderItem?>), typeof(OrderItemsWindow));

        public OrderItemsWindow(int orderID)
        {
            orderItemsForList = bl.Order.Get(x => x?.ID == orderID).Items;
            InitializeComponent();
        }

    }
}
