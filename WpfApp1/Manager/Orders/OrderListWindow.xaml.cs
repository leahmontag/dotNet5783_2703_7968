using BO;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public IEnumerable<BO.OrderForList?> orderForList
        {
            get { return (IEnumerable<BO.OrderForList?>)GetValue(orderProperty); }
            set { SetValue(orderProperty, value); }
        }
        public static readonly DependencyProperty orderProperty =
           DependencyProperty.Register(nameof(orderForList), typeof(IEnumerable<BO.OrderForList?>), typeof(OrderListWindow));
        public OrderForList selectedOrder { get; set; }

        public OrderListWindow()
        {
            orderForList = bl.Order.GetAll();
            InitializeComponent();
        }

        private void OrdersListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Close();
            new OrderWindow(selectedOrder.ID).Show();
        }
    }
}


