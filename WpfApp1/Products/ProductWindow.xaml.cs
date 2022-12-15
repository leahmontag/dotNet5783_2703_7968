using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;


namespace PL.Products
{
    /// <summary>
    /// Interaction logic for BoProductWindow.xaml
    /// </summary>
    public partial class BoProductWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public BoProductWindow(string buttonAdd)
        {
            InitializeComponent();
            int val = 0;
            Btn.Content = buttonAdd;
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            Price.Text = val.ToString();
            InStock.Text = val.ToString();
        }
        public BoProductWindow(string buttonAdd, int productID)
        {
            InitializeComponent();
            try
            {
                Btn.Content = buttonAdd;
                if (Btn.Content == "Update")
                    Id.IsReadOnly = true;
                BO.Product product = bl.Product.GetByManager(x => x?.ID == productID);
                Id.Text = product.ID.ToString();
                Name.Text = product.Name;
                Price.Text = product.Price.ToString();
                InStock.Text = product.InStock.ToString();
                CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
                CategorySelector.SelectedItem = product.Category;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ChoiceOfButten_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content == "Add")
            {
                try
                {
                    AddNewProductButton_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if ((sender as Button).Content == "Update")
            {
                try
                {
                    UpdateProductButton_Click(sender, e);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return;
        }
        private void AddNewProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(new Regex("^[0-9]+$")).IsMatch(Price.Text) || !(new Regex("^[0-9]+$")).IsMatch(InStock.Text))//input validity
            {
                MessageBox.Show("wrong format");
            }
            bl.Product.Create(new BO.Product()
            {
                ID = int.Parse(Id.Text),
                Name = Name.Text,
                Category = (BO.Enums.Category)CategorySelector.SelectedItem,
                Price = double.Parse(Price.Text),
                InStock = int.Parse(InStock.Text),
            });
            this.Close();
            new BoProductListWindow().Show();
        }
        private void UpdateProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(new Regex("^[0-9]+$")).IsMatch(Price.Text) || !(new Regex("^[0-9]+$")).IsMatch(InStock.Text))//input validity
            {
                MessageBox.Show("wrong format");
            }
            bl.Product.Update(new BO.Product()
            {
                ID = int.Parse(Id.Text),
                Name = Name.Text,
                Category = (BO.Enums.Category)CategorySelector.SelectedItem,
                Price = double.Parse(Price.Text),
                InStock = int.Parse(InStock.Text),
            }); ;
            this.Close();
            new BoProductListWindow().Show();
        }
    }
}