using BlApi;
using BlImplementation;
using BO;
using System;
using System.Windows;
using System.Windows.Controls;
using static BO.Enums;


namespace PL.BoProducts
{
    /// <summary>
    /// Interaction logic for BoProductWindow.xaml
    /// </summary>
    public partial class BoProductWindow : Window
    {
        private IBl bl = new Bl();
        public BoProductWindow(string buttonAdd)
        {
            InitializeComponent();
            int val = 0;
            Btn.Content = buttonAdd;
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            Price.Text = val.ToString();
            InStock.Text = val.ToString();
        }
        public BoProductWindow(string buttonAdd,int productID)
        {
            InitializeComponent();
            Btn.Content = buttonAdd;
            if(Btn.Content=="Update")
                Id.IsReadOnly=true;
            BO.Product product= bl.Product.GetByManager(productID);
            Id.Text = product.ID.ToString();
            Name.Text = product.Name;
            Price.Text= product.Price.ToString();
            InStock.Text = product.InStock.ToString();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            CategorySelector.SelectedItem = product.Category;

        }
        private void ChoiceOfButten_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content == "Add")
            {
                AddNewProductButton_Click(sender, e);
            }
            else if ((sender as Button).Content == "Update")
                UpdateProductButton_Click(sender, e);
            return;
        }
        private void AddNewProductButton_Click(object sender, RoutedEventArgs e)
        {
            bl.Product.Create(new BO.Product()
            {
                ID = int.Parse(Id.Text),
                Name = Name.Text,
                Category = (BO.Enums.Category)CategorySelector.SelectedItem,
                Price = double.Parse(Price.Text),
                InStock = int.Parse(InStock.Text),
            });
            new BoProductListWindow().Show();
        }
        private void UpdateProductButton_Click(object sender, RoutedEventArgs e) 
        {
            bl.Product.Update(new BO.Product()
            {
                ID = int.Parse(Id.Text),
                Name = Name.Text,
                Category = (BO.Enums.Category)CategorySelector.SelectedItem,
                Price = double.Parse(Price.Text),
                InStock = int.Parse(InStock.Text),
            }); ;
            new BoProductListWindow().Show();
        }
    }
}
