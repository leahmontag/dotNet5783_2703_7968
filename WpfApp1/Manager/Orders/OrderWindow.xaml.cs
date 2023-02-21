using System;
using System.Windows;


namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public BO.Order order
        {
            get { return (BO.Order)GetValue(orderProperty); }
            set { SetValue(orderProperty, value); }
        }
        public static readonly DependencyProperty orderProperty =
           DependencyProperty.Register(nameof(order), typeof(BO.Order), typeof(OrderWindow));
        public BO.Order selectedOrder { get; set; }

        public bool VisibileShipping
        {
            get { return (bool)GetValue(VisibileShippingProperty); }
            set { SetValue(VisibileShippingProperty, value); }
        }
        public static readonly DependencyProperty VisibileShippingProperty =
           DependencyProperty.Register(nameof(VisibileShipping), typeof(bool), typeof(OrderWindow));

        public bool VisibileDelivery
        {
            get { return (bool)GetValue(VisibileDeliveryProperty); }
            set { SetValue(VisibileDeliveryProperty, value); }
        }
        public static readonly DependencyProperty VisibileDeliveryProperty =
           DependencyProperty.Register(nameof(VisibileDelivery), typeof(bool), typeof(OrderWindow));

        public string errorProp
        {
            get { return (string)GetValue(errorPropProperty); }
            set { SetValue(errorPropProperty, value); }
        }
        public static readonly DependencyProperty errorPropProperty =
          DependencyProperty.Register(nameof(errorProp), typeof(string), typeof(OrderWindow));


        public OrderWindow(int orderID)
        {
            try
            {
                VisibileShipping = false;
                VisibileDelivery = false;
                order = bl.Order.Get(x => x?.ID == orderID);
                InitializeComponent();
            }
            catch (Exception ex)
            {
                errorProp = ex.Message;
            }

            if (order.Status.ToString() == "provided")
            {
                VisibileShipping = true;
                VisibileDelivery = true;
            }
            else if (order.Status.ToString() == "send")
                VisibileShipping = true;
            else
                VisibileDelivery = true;
        }

        private void ChoiceOfButten_Click3(object sender, RoutedEventArgs e)
        {
            this.Close();
            new OrderItemsWindow(order.ID).Show();
        }

        private void updateDeliveryBtn_Click(object sender, RoutedEventArgs e)
        {
            VisibileDelivery = true;
            order = bl.Order.UpdateDelivery(order.ID);
        }

        private void updateShippingBtn_Click(object sender, RoutedEventArgs e)
        {
            VisibileShipping = true;
            VisibileDelivery = false;
            order = bl.Order.UpdateShip(order.ID);
        }
    }
}
