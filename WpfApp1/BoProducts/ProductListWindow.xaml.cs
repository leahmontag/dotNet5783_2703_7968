using BlApi;
using BlImplementation;
using System;
using System.Collections.Generic;
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

namespace PL.BoProducts
{
    /// <summary>
    /// Interaction logic for BoProductListWindow.xaml
    /// </summary>
    public partial class BoProductListWindow : Window
    {
        private IBl bl = new Bl();
        public BoProductListWindow()
        {
            InitializeComponent();
            ProductsListView.ItemsSource = bl.Product.GetAll();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            
        }
   
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {}

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedItem = CategorySelector.SelectedItem.ToString();
            ProductsListView.ItemsSource = bl.Product.GetAllByCategory(selectedItem);

            
        }

        private void addNewProductButton_Click(object sender, RoutedEventArgs e) => new BoProductWindow().Show();
    }
}
