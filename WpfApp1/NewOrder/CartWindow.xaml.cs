using BlApi;
using BO;
using DO;
using PL.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using WpfApp1;

namespace PL.NewOrder;

/// <summary>
/// Interaction logic for CartWindow.xaml
/// </summary>
public partial class CartWindow : Window
{
    static BlApi.IBl? bl = BlApi.Factory.Get();
    public BO.Cart cart
    {
        get { return (BO.Cart)GetValue(cartProperty); }
        set { SetValue(cartProperty, value); }
    }
    public static readonly DependencyProperty cartProperty =
       DependencyProperty.Register(nameof(cart), typeof(BO.Cart), typeof(CartWindow));

    public ObservableCollection<BO.OrderItem?> items

    {
        get { return (ObservableCollection<BO.OrderItem?>)GetValue(ItemsProperty); }
        set
        {
            SetValue(ItemsProperty, value);
            cart ??= new();
            cart.Items ??= new();
            cart.Items = items.Select(x => x).ToList();
        }
    }
    public static readonly DependencyProperty ItemsProperty =
       DependencyProperty.Register(nameof(items), typeof(ObservableCollection<BO.OrderItem?>), typeof(CartWindow));


    public BO.ProductItem product
    {
        get { return (BO.ProductItem)GetValue(productProperty); }
        set { SetValue(productProperty, value); }
    }
    public static readonly DependencyProperty productProperty =
       DependencyProperty.Register(nameof(product), typeof(BO.ProductItem), typeof(CartWindow));
    public string errorProp
    {
        get { return (string)GetValue(errorPropProperty); }
        set { SetValue(errorPropProperty, value); }
    }
    public static readonly DependencyProperty errorPropProperty =
      DependencyProperty.Register(nameof(errorProp), typeof(string), typeof(CartWindow));
    public string expMargin
    {
        get { return (string)GetValue(expMarginProperty); }
        set { SetValue(expMarginProperty, value); }
    }
    public static readonly DependencyProperty expMarginProperty =
      DependencyProperty.Register(nameof(expMargin), typeof(string), typeof(CartWindow));

    private Action<ProductItem?> action;

    public CartWindow(BO.Cart updateingCart, Action<ProductItem?> action)
    {
        items = new(updateingCart.Items);
        cart = updateingCart;
        InitializeComponent();
        this.action = action;
    }

    private void BtnConfirmOrder_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (cart.CustomerEmail == null || !new Regex("[@]").IsMatch(cart.CustomerEmail.ToString()) || !new Regex("[.]").IsMatch(cart.CustomerEmail.ToString()))
            {
                expMargin = "596,218,0,0";
                throw new Exception("wrong format of Email");
            }
            else if (cart.CustomerName == null)
            {
                expMargin = "596,264,0,0";
                throw new Exception("wrong format of name");
            }
            else if (cart.CustomerAddress == null)
            {
                expMargin = "596,171,0,0";
                throw new Exception("wrong format of address");
            }
            else if (cart.TotalPrice == 0)
            {
                expMargin = "549,347,0,0";
                throw new Exception("your cart is empty!");

            }
            BO.Cart c = cart;
            bl.Cart.ConfirmOrder(cart);
            //Close();
            App.Current.Shutdown();

            new ConfirmOrderWindow(c).Show();
        }
        catch (BO.ProductIsNotAvailableException exp)
        {
            errorProp = exp.Message;
        }
        catch (Exception exp)
        {
            errorProp = exp.Message;
        }
    }
    private void BtnRemoveItem(object sender, RoutedEventArgs e)
    {
        var element = e.OriginalSource as FrameworkElement;

        if (element != null && element.DataContext is BO.OrderItem)
        {
            if (cart!.Items != null && bl != null)
            {
                try
                {
                    product = bl.Product.GetProductFromCatalog(cart, x => x?.ID.ToString() == (element.DataContext as BO.OrderItem)!.ProductID.ToString());
                    product.Amount = 0;
                    actionFunction();
                    items = new( bl.Cart.Update(cart, (element.DataContext as BO.OrderItem)!.ProductID, 0).Items);
                    Close();
                }
                catch (BO.FailedToDisplayAllItemsException exp)
                {
                    errorProp = exp.Message;
                }
                catch (Exception exp)
                {
                    errorProp = exp.Message;
                }
            }

        }
    }
    private void btnAddAmount(object sender, RoutedEventArgs e)
    {
        var element = e.OriginalSource as FrameworkElement;

        if (element != null && element.DataContext is BO.OrderItem)
        {
            if (items != null && bl != null)
            {
                try
                {
                   items = new( bl.Cart.Update(cart, (element.DataContext as BO.OrderItem)!.ProductID, (element.DataContext as BO.OrderItem)!.Amount + 1).Items);

                   product = bl.Product.GetProductFromCatalog(cart, x => x?.ID.ToString() == (element.DataContext as BO.OrderItem)!.ProductID.ToString());
                  //  actionFunction();
                }
                catch (BO.ProductIsNotAvailableException exp)
                {
                    errorProp = exp.Message;
                }
                catch (Exception exp)
                {
                    errorProp = exp.Message;
                }
            }

        }
    }


    private void btnSubtractAmount(object sender, RoutedEventArgs e)
    {
        var element = e.OriginalSource as FrameworkElement;
        if (element != null && element.DataContext is BO.OrderItem)
        {
            if (cart!.Items != null && bl != null)
            {
                try
                {
                    items =new( bl.Cart.Update(cart, (element.DataContext as BO.OrderItem)!.ProductID, (element.DataContext as BO.OrderItem)!.Amount - 1).Items);
                    product = bl.Product.GetProductFromCatalog(cart, x => x?.ID.ToString() == (element.DataContext as BO.OrderItem)!.ProductID.ToString());
                    actionFunction();
                }
                catch (BO.ProductIsNotAvailableException exp)
                {
                    errorProp = exp.Message;
                }
                catch (Exception exp)
                {
                    errorProp = exp.Message;
                }
            }

        }
    }
    private void actionFunction()
    {

        action(new ProductItem
        {
            ID = product.ID,
            Amount = product.Amount,
            Name = product.Name,
            Price = product.Price,
            InStock = product.InStock,
            Category = product.Category
        });
    }
}

