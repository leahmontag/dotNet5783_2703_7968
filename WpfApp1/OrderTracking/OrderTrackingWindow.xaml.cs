using Amazon.DynamoDBv2;
using BO;
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
        public string errorProp
        {
            get { return (string)GetValue(errorPropProperty); }
            set { SetValue(errorPropProperty, value); }
        }
        public static readonly DependencyProperty errorPropProperty =
          DependencyProperty.Register(nameof(errorProp), typeof(string), typeof(OrderTrackingWindow));

        public string orderTrakingNum
        {
            get { return (string)GetValue(orderTrakingNumProperty); }
            set { SetValue(orderTrakingNumProperty, value); }
        }
        public static readonly DependencyProperty orderTrakingNumProperty =
          DependencyProperty.Register(nameof(orderTrakingNum), typeof(string), typeof(OrderTrackingWindow));


        public OrderTrackingWindow()
        {
            InitializeComponent();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (orderTrakingNum == null)//לבדוק לגבי הבדיקה
                {
                    throw new Exception("order traking number can't be null");

                }
                else if (!(new Regex("^[0-9]+$")).IsMatch(orderTrakingNum.ToString()) || int.Parse(orderTrakingNum) <= 0)
                    throw new Exception("wrong format");
                else
                    new OrderTrackingdetailsWindow(int.Parse(orderTrakingNum)).Show();

                orderTrakingNum = null;
            }
            catch (Exception ex)
            {
                orderTrakingNum = null;
                errorProp = ex.Message;
            }
        }

        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            errorProp = "";
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
