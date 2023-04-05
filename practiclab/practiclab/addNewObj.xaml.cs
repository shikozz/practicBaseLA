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
    /// Interaction logic for addNewObj.xaml
    /// </summary>
    public partial class addNewObj : Window
    {
        private MainWindow mainwindow;
        public addNewObj(MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = this;
            mainwindow = mainWindow;
        }

        private void addProd_Click(object sender, RoutedEventArgs e)
        {
            //Создание нового продукта и наполнение его данными
            var newProd = new Base.Product();
            newProd.ProductArticleNumber = article.Text;
            newProd.ProductName = name.Text;
            newProd.ProductDescription = description.Text;
            newProd.ProductCategory = category.Text;
            newProd.ProductManufacturer = manufacturer.Text;
            newProd.ProductCost = Convert.ToDecimal(cost.Text);
            newProd.ProductDiscountAmount = Convert.ToByte(discount.Text);
            newProd.ProductQuantityInStock = Convert.ToByte(amount.Text);
            newProd.ProductStatus = status.Text;
            SourceCore.MyBase.Product.Add(newProd);
            SourceCore.MyBase.SaveChanges();
            //Обновление списока продуктов с главной страницы
            mainwindow.UpdateList(null);
            mainwindow.prodList.SelectedItem = newProd;
            mainwindow.prodList.UpdateLayout();
            mainwindow.prodList.ScrollIntoView(mainwindow.prodList.SelectedItem);
            Close();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
