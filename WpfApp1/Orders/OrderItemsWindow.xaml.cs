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
        private ObservableCollection<BO.OrderItem?> _myOrderItems;

        public OrderItemsWindow(int orderID)
        {
            InitializeComponent();
            BO.Order?  _myOrder = bl.Order.Get(x => x?.ID == orderID);
            _myOrderItems = new(_myOrder.Items);
           DataContext = _myOrderItems;
        }

    }
}
