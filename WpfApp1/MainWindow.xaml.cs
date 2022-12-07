using BlApi;
using BL;

using System.Windows;
using PL.BoProducts;
using BlImplementation;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IBl bl = new Bl();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ShowProductsButton_Click(object sender, RoutedEventArgs e) => new BoProductListWindow().Show();
    }
}