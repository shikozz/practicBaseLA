using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace practiclab
{
    /// <summary>
    /// Interaction logic for redact.xaml
    /// </summary>
    public partial class redact : Window
    {
        private MainWindow mainwindow;
        private Base.Product SelectedProd;
        private Base.practic_LAEntities DataBase;
        public redact(Base.Product selectedProd, MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = this;
            SelectedProd = selectedProd;
            mainwindow = mainWindow;
            DataBase = new Base.practic_LAEntities();
            init();
        }

        public void init()
        {
            Base.Product setProduct = DataBase.Product.SingleOrDefault(U => U.ProductArticleNumber == SelectedProd.ProductArticleNumber);
            article.Text = setProduct.ProductArticleNumber;
            name.Text = setProduct.ProductName;
            description.Text = setProduct.ProductDescription;
            category.Text = setProduct.ProductCategory;
            manufacturer.Text = setProduct.ProductManufacturer;
            cost.Text = setProduct.ProductCost.ToString();
            discount.Text = setProduct.ProductDiscountAmount.ToString();
            amount.Text = setProduct.ProductQuantityInStock.ToString();
            status.Text = setProduct.ProductStatus;
        }

        private void addProd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var EditProd = new Base.Product();
                EditProd = SourceCore.MyBase.Product.First(P=>P.ProductArticleNumber == SelectedProd.ProductArticleNumber);
                EditProd.ProductArticleNumber = article.Text;
                EditProd.ProductName = name.Text;
                EditProd.ProductDescription = description.Text;
                EditProd.ProductCategory = category.Text;
                EditProd.ProductManufacturer = manufacturer.Text;
                EditProd.ProductCost = Convert.ToDecimal(cost.Text);
                EditProd.ProductDiscountAmount = Convert.ToByte(discount.Text);
                EditProd.ProductQuantityInStock = Convert.ToByte(amount.Text);
                EditProd.ProductStatus = status.Text;
                SourceCore.MyBase.SaveChanges();
                mainwindow.UpdateList(null);
                mainwindow.prodList.SelectedItem = EditProd;
                mainwindow.prodList.UpdateLayout();
                mainwindow.prodList.ScrollIntoView(mainwindow.prodList.SelectedItem);
                Close();
            }
            catch
            {

            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
