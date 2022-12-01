using BlApi;
using BlImplementation;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Windows;
using static BO.Enums;


namespace PL.BoProducts
{
    /// <summary>
    /// Interaction logic for BoProductWindow.xaml
    /// </summary>
    public partial class BoProductWindow : Window
    {
        private IBl bl = new Bl();
        public BoProductWindow()
        {
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        }

        private void CategorySelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) { }
        private void AddNewProductButton_Click(object sender, RoutedEventArgs e)
        {
            bl.Product.Create(new BO.Product()
            {
                ID = int.Parse(Id.Text),
                Name = Name.Text,
                // Category = Enum.TryParse(CategorySelector.Items, out CategorySelector),
                Category = Category.eyeMakeup,
                Price = double.Parse(Price.Text),
                InStock = int.Parse(InStock.Text),
            }) ;
            new BoProductListWindow().Show();
        }
    }
}
