using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        static BlApi.IBl? bl = BlApi.Factory.Get();
        public ObservableCollection<BO.ProductForList?> productListTemp;

        public ObservableCollection<BO.ProductForList?> productForList

        {
            get { return (ObservableCollection<BO.ProductForList?>)GetValue(productForListsProperty); }
            set { SetValue(productForListsProperty, value); }
        }
        public static readonly DependencyProperty productForListsProperty =
           DependencyProperty.Register(nameof(productForList), typeof(ObservableCollection<BO.ProductForList?>), typeof(ProductListWindow));
        public bool filtering

        {
            get { return (bool)GetValue(filteringProperty); }
            set { SetValue(filteringProperty, value); }
        }
        public static readonly DependencyProperty filteringProperty =
           DependencyProperty.Register(nameof(filtering), typeof(bool), typeof(ProductListWindow));
        public BO.Enums.Category? selectedCategory

        {
            get { return (BO.Enums.Category?)GetValue(selectedCategoryProperty); }
            set { SetValue(selectedCategoryProperty, value); }
        }
        public static readonly DependencyProperty selectedCategoryProperty =
           DependencyProperty.Register(nameof(selectedCategory), typeof(BO.Enums.Category?), typeof(ProductListWindow));
        public System.Array categories { get; set; } = Enum.GetValues(typeof(BO.Enums.Category));
        public ProductForList selectedProduct { get; set; } = new();
        public ProductListWindow()
        {
            selectedCategory = null;
            productForList = new ObservableCollection<BO.ProductForList?>(bl.Product.GetAll().Cast<BO.ProductForList?>());
            productListTemp = productForList;
            InitializeComponent();
        }


        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            productForList = productListTemp;
            var GropupingProducts = (from p in productForList
                                     where p.Category == selectedCategory
                                     group p by p.Category into catGroup
                                     from pr in catGroup
                                     select pr).ToList();
            productForList = new(GropupingProducts);
        }

        private void addNewProductButton_Click(object sender, RoutedEventArgs e)
        {
            string Btn = "Add";
            new ProductWindow(Btn, add).Show();
        }

        private void ProductsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string Btn = "Update";
            new ProductWindow(Btn, selectedProduct.ID, update).Show();
        }
        public void update(ProductForList? productUpdate)
        {
            var item = productForList.FirstOrDefault(item => item?.ID == productUpdate?.ID);
            if (item != null)
            {
                productForList[productForList.IndexOf(item)] = productUpdate;
                productListTemp[productListTemp.IndexOf(item)] = productUpdate;
            }
        }
        public void add(ProductForList? productUpdate)
        {
            productForList.Insert(productForList.Count, productUpdate);
            productListTemp.Insert(productListTemp.Count, productUpdate);
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CheckBox_checked(object sender, EventArgs e)
        {
            selectedCategory = null;
            productForList = productListTemp;
            filtering = false;
            //System.Timers.Timer timer = new System.Timers.Timer();
            //timer.Interval = 3000;
            //timer.Elapsed += timer_Elapsed;
            //timer.Start();
        }
        //private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    filtering = false;
        //}
    }
}
