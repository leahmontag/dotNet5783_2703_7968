using BlApi;
using BL;

using System.Windows;
using PL.Products;
using BlImplementation;

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
        private void ShowProductsButton_Click(object sender, RoutedEventArgs e) => new BoProductListWindow().Show();
    }
}