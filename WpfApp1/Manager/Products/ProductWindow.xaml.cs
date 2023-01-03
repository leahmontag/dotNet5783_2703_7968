using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;


namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public ProductWindow(string buttonAdd)
        {
            InitializeComponent();
            int val = 0;
            Btn.Content = buttonAdd;
            CategorySelector.DataContext = Enum.GetValues(typeof(BO.Enums.Category));
            Price.Text = val.ToString();
            InStock.Text = val.ToString();
        }
        public ProductWindow(string button, int productID)
        {
            InitializeComponent();
            try
            {
                Btn.Content = button;
                if (Btn.Content == "Update")
                    Id.IsReadOnly = true;
                BO.Product product = bl.Product.GetByManager(x => x?.ID == productID);
                gridProduct.DataContext = product;
                CategorySelector.DataContext = Enum.GetValues(typeof(BO.Enums.Category));
                CategorySelector.SelectedItem = product.Category;
                CategorySelector.SelectedValue = product.Category;
                CategorySelector.SelectedValuePath = product.Category.ToString();
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
            try
            {
                if ((!(new Regex("^[0-9]+$")).IsMatch(Price.Text) || (int.Parse(Price.Text) <= 0)) || !(new Regex("^[0-9]+$")).IsMatch(InStock.Text) || (int.Parse(Price.Text) <= 0))//input validity
                {
                    throw new Exception("wrong format");
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
                new ProductListWindow().Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void UpdateProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((!(new Regex("^[0-9]+$")).IsMatch(Price.Text) || (int.Parse(Price.Text) <= 0)) || !(new Regex("^[0-9]+$")).IsMatch(InStock.Text) || (int.Parse(Price.Text) <= 0))//input validity
                {
                    throw new Exception("wrong format");
                }
                bl.Product.Update(new BO.Product()
                {
                    ID = int.Parse(Id.Text),
                    Name = Name.Text,
                    Category = (BO.Enums.Category)CategorySelector.SelectedItem,
                    Price = double.Parse(Price.Text),
                    InStock = int.Parse(InStock.Text),
                }); 
                this.Close();
                new ProductListWindow().Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}