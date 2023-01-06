using BO;
using PL.Orders;
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

namespace PL.OrderTracking
{
    /// <summary>
    /// Interaction logic for OrderTrackingdetailsWindow.xaml
    /// </summary>
    public partial class OrderTrackingdetailsWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public BO.OrderTracking orderTracking
        {
            get { return (BO.OrderTracking)GetValue(orderTrackingProperty); }
            set { SetValue(orderTrackingProperty, value); }
        }
        public static readonly DependencyProperty orderTrackingProperty =
           DependencyProperty.Register(nameof(orderTracking), typeof(BO.OrderTracking), typeof(OrderTrackingdetailsWindow));
        
        public OrderTrackingdetailsWindow(int OrderTrackingID)
        {
            orderTracking = bl.Order.TrackingOfOrder(OrderTrackingID);
            InitializeComponent();
        }

        private void ChoiceOfButten_Click3(object sender, RoutedEventArgs e)
        {
            new OrderWindow(orderTracking.ID).Show();
        }
    }
}
