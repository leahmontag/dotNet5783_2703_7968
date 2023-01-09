using BO;
using DO;
using System;
using System.Collections.Generic;
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
        public BO.Product product
        {
            get { return (BO.Product)GetValue(productProperty); }
            set { SetValue(productProperty, value); }
        }
        public static readonly DependencyProperty productProperty =
           DependencyProperty.Register(nameof(product), typeof(BO.Product), typeof(ProductWindow));

        public System.Array CategorySelector { get; set; } = Enum.GetValues(typeof(BO.Enums.Category));
        public string buttonContent { get; set; }
        public bool isReadOnly { get; set; } = false;


        public ProductWindow(string buttonAdd)
        {
            buttonContent = buttonAdd;
            product = new BO.Product();
            InitializeComponent();
        }
        public ProductWindow(string button, int productID)
        {
            buttonContent = button;
            try
            {
                product = bl.Product.GetByManager(x => x?.ID == productID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            isReadOnly = true;
            InitializeComponent();
        }
        private void ChoiceOfButten_Click(object sender, RoutedEventArgs e)
        {
            if (buttonContent == "Add")
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
            else if (buttonContent == "Update")
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
                if ((!(new Regex("^[0-9]+$")).IsMatch(product.Price.ToString()) || (int.Parse(product.Price.ToString()) <= 0)) || !(new Regex("^[0-9]+$")).IsMatch(product.InStock.ToString()) || (int.Parse(product.InStock.ToString()) <= 0))//input validity
                {
                    throw new Exception("wrong format");
                }
                bl.Product.Create(product);
                Close();
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
                if ((!(new Regex("^[0-9]+$")).IsMatch(product.Price.ToString()) || (int.Parse(product.Price.ToString()) <= 0)) || !(new Regex("^[0-9]+$")).IsMatch(product.InStock.ToString()) || (int.Parse(product.InStock.ToString()) <= 0))//input validity
                {
                    throw new Exception("wrong format");
                }
                bl.Product.Update(product);
                Close();
                new ProductListWindow().Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}