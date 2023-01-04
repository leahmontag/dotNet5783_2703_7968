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
        private ObservableCollection<BO.OrderTracking?> _myOrderItems;
        public OrderTrackingdetailsWindow(int OrderTrackingID)
        {
            InitializeComponent();
            BO.OrderTracking? _myOrder = bl.Order.TrackingOfOrder(OrderTrackingID);
            gridOrder.DataContext = _myOrder;
            Grid1.DataContext = _myOrder.OrderTrackingDateAndDesc[0];
            Grid2.DataContext = _myOrder.OrderTrackingDateAndDesc[1];
            Grid3.DataContext = _myOrder.OrderTrackingDateAndDesc[2];
        }

        private void ChoiceOfButten_Click3(object sender, RoutedEventArgs e)
        {
            new OrderWindow(int.Parse(ID.Text)).Show();
        }
    }
}
