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

namespace PL.NewOrder
{
    /// <summary>
    /// Interaction logic for NewOrderWindow.xaml
    /// </summary>
    public partial class NewOrderWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        private ObservableCollection<BO.ProductForList?> _myCollection;

        BO.Cart cartBL = new();
        public NewOrderWindow()
        {
            InitializeComponent();
            _myCollection = new(bl.Product.GetAll());
            ProductItemListView.DataContext = _myCollection;
            CategorySelector.DataContext = Enum.GetValues(typeof(BO.Enums.Category));
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string? selectedItem = CategorySelector.SelectedItem.ToString();
            _myCollection = new(bl.Product.GetAll(x => x?.Category.ToString() == selectedItem));
            ProductItemListView.DataContext = _myCollection;
        }
    }
}
