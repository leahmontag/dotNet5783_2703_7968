using Amazon.DynamoDBv2;
using DO;
using PL.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL.OrderTracking
{
    /// <summary>
    /// Interaction logic for OrderTrackingWindow.xaml
    /// </summary>
    public partial class OrderTrackingWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public int? orderTrakingNum { get; set; }
        public OrderTrackingWindow()
        {
            // orderTrakingNum = null;
            InitializeComponent();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            Close();
            try
            {
                if (!(new Regex("^[0-9]+$")).IsMatch(orderTrakingNum.ToString()) || orderTrakingNum <= 0)
                    throw new Exception("wrong format");

                if (orderTrakingNum.HasValue)//לבדוק לגבי הבדיקה
                    new OrderTrackingdetailsWindow((int)orderTrakingNum).Show();
                else
                    throw new Exception("order traking number can't be null");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
