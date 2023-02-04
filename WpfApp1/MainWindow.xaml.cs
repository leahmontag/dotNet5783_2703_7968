using BlApi;
using BL;

using System.Windows;
using PL.Products;
using PL.Orders;
using BlImplementation;
using PL.ManagerVew;
using PL.NewOrder;
using PL.OrderTracking;
using PL.registeredUser;
using PL.Simulator;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowManagerWindowButton_Click(object sender, RoutedEventArgs e) => new ManagerWindow().Show();
        private void ShowNewOrderWindowButton_Click(object sender, RoutedEventArgs e) => new NewOrderWindow().Show();
        private void ShowOrderTrackingWindowButton_Click(object sender, RoutedEventArgs e) => new OrderTrackingWindow().Show();
        private void ShowregisteredUserWindowButton_Click(object sender, RoutedEventArgs e) => new registeredUserWindow().Show();
        private void ShowSimulatorButton_Click(object sender, RoutedEventArgs e) => new SimulatorWindow().Show();

    }
}

