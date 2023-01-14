using BO;
using DO;
using PL.OrderTracking;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.Products;

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
    public string errorProp
    {
        get { return (string)GetValue(errorPropProperty); }
        set { SetValue(errorPropProperty, value); }
    }
    public static readonly DependencyProperty errorPropProperty =
      DependencyProperty.Register(nameof(errorProp), typeof(string), typeof(ProductWindow));

    public string expMargin
    {
        get { return (string)GetValue(expMarginProperty); }
        set { SetValue(expMarginProperty, value); }
    }
    public static readonly DependencyProperty expMarginProperty =
      DependencyProperty.Register(nameof(expMargin), typeof(string), typeof(ProductWindow));

    public System.Array CategorySelector { get; set; } = Enum.GetValues(typeof(BO.Enums.Category));
    public string buttonContent { get; set; }
    public bool isReadOnly { get; set; } = false;

    private Action<ProductForList?> action;
    public ProductWindow(string buttonAdd, Action<ProductForList?> action)
    {
        buttonContent = buttonAdd;
        product = new BO.Product();
        InitializeComponent();
        this.action = action;
    }
    public ProductWindow(string button, int productID, Action<ProductForList?> action)
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
        this.action = action;
    }
    private void ChoiceOfButten_Click(object sender, RoutedEventArgs e)
    {
        if (buttonContent == "Add")
        {
            try
            {
                AddNewProductButton_Click(sender, e);
                // action(bl.Product.GetProductForList(x => x?.ID == product.ID));
                action(new ProductForList
                {
                    Price = product.Price,
                    ID = product.ID,
                    Name = product.Name,
                    Category = product.Category,
                });
            }
            catch (Exception ex)
            {
                errorProp = ex.Message;
            }
        }
        else if (buttonContent == "Update")
        {
            try
            {
                UpdateProductButton_Click(sender, e);
                // action(bl.Product.GetProductForList(x => x?.ID == product.ID));
                action(new ProductForList
                {
                    Price = product.Price,
                    ID = product.ID,
                    Name = product.Name,
                    Category = product.Category,
                });
            }
            catch (Exception ex)
            {
                errorProp = ex.Message;
            }
        }
        return;
    }
    private void AddNewProductButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            inputValidity();
            bl.Product.Create(product);
            Close();
        }
        catch (Exception ex)
        {
            errorProp = ex.Message;
        }
    }
    private void UpdateProductButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            inputValidity();
            bl.Product.Update(product);
            Close();
        }
        catch (Exception ex)
        {
            errorProp = ex.Message;
        }

    }
    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }
    private void inputValidity()
    {
        if ((!(new Regex("^[0-9]+$")).IsMatch(product.Price.ToString()) || (int.Parse(product.Price.ToString()) <= 0)))
        {
            expMargin = "304,210,0,0";
            throw new Exception("wrong format of price");
        }
        else if (!(new Regex("^[0-9]+$")).IsMatch(product.InStock.ToString()) || (int.Parse(product.InStock.ToString()) <= 0))
        {
            expMargin = "307,252,0,0";
            throw new Exception("wrong format of inStock");
        }
        else if (product.Name == null || product.Name == "")
        {
            expMargin = "310,168,0,0";
            throw new Exception("wrong format of name");
        }
        else if (product.ID == null || product.ID == 0)
        {
            expMargin = "299,85,0,0";
            throw new Exception("wrong format of id");
        }
        else if (product.Category == null)
        {
            expMargin = "315,124,0,0";
            throw new Exception("wrong format of category");
        }
    }
}
