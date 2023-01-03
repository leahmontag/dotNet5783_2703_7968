using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
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
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        int IDOrder;
        public OrderWindow(int orderID)
        {
            InitializeComponent();
            try
            {
                BO.Order order = bl.Order.Get(x => x?.ID == orderID);
                gridOrder.DataContext = order;
                IDOrder = order.ID;
                if (order.Status.ToString() == "provided")
                {
                    updateShippingBtn.Visibility = Visibility.Collapsed;
                    updateDeliveryBtn.Visibility = Visibility.Collapsed;
                }
                else if(order.Status.ToString() == "send")
                    updateShippingBtn.Visibility = Visibility.Collapsed;
                else
                    updateDeliveryBtn.Visibility = Visibility.Collapsed;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

     

     
        private void ChoiceOfButten_Click3(object sender, RoutedEventArgs e)
        {
            this.Close();
            new OrderItemsWindow(IDOrder).Show();
        }

        private void updateDeliveryBtn_Click(object sender, RoutedEventArgs e)
        {
            bl.Order.UpdateDelivery(IDOrder);
            Close();
            new OrderWindow(IDOrder).Show();
        }

        private void updateShippingBtn_Click(object sender, RoutedEventArgs e)
        {
            bl.Order.UpdateShip(IDOrder);
            Close();
            new OrderWindow(IDOrder).Show();
        }
    }
}
