using BlApi;
using BlImplementation;
using BO;
using DO;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2016.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
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
            CategorySelector.Text ="NONE";
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
            CategorySelector.Text = product.Category.ToString();
        }

        private void CategorySelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) { }
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
                //    Category = Enum.TryParse(typeof(Category), CategorySelector.Items),
                Category = Category.eyeMakeup,
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
                //    Category = Enum.TryParse(typeof(Category), CategorySelector.Items),
                Category = Category.eyeMakeup,
                Price = double.Parse(Price.Text),
                InStock = int.Parse(InStock.Text),
            });
            new BoProductListWindow().Show();
        }

    }
}
