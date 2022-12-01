using BlApi;
using BlImplementation;
using System;
using System.Windows;


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

            var ID = Id.Text;


            bl.Product.Create(new BO.Product()
            {
                // ID=,
                //      Name = Name.Text,
                //    Category=,
                //  Price=,
                //InStock=,


            });
        }
    }
}
