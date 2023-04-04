using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace practiclab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Base.Product SelectedProd;
        private int roleUser = 2;
        private bool sortOrder = true;
        public MainWindow(int role)
        {
            InitializeComponent();
            roleUser = role;
            fio.Content = "Пользователь: ";
            addbtn.Background = new SolidColorBrush(Color.FromRgb(118, 227, 131));
            redactbtn.Background = new SolidColorBrush(Color.FromRgb(118, 227, 131));
            delbtn.Background = new SolidColorBrush(Color.FromRgb(118, 227, 131));
            exit.Background = new SolidColorBrush(Color.FromRgb(118, 227, 131));
            if (roleUser!=1)
            {
                adminpanel.Visibility = Visibility.Hidden;
               
            }

            if(roleUser==10)
            {
                fio.Content += "Гость";
            }
            else
            {
                Base.User newUser = SourceCore.MyBase.User.SingleOrDefault(Q=>Q.UserID==roleUser);
                fio.Content += newUser.UserName + " " + newUser.UserSurname + " " + newUser.UserPatronymic;
            }
            UpdateList(null);
        }

        public void UpdateList(Base.Product Product)
        {
            if((Product==null)&&(prodList.ItemsSource!=null))
            {
                Product = (Base.Product)prodList.SelectedItem;
            }

            ObservableCollection<Base.Product> Products =
            new ObservableCollection<Base.Product>(SourceCore.MyBase.Product.ToList());
            prodList.ItemsSource = Products;
            prodList.SelectedItem = Product;
        }

        private void prodList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void addbtnclick(object sender, RoutedEventArgs e)
        {
            addNewObj newWindow = new addNewObj(this);
            newWindow.ShowDialog();
        }

        private void delbtn_Click(object sender, RoutedEventArgs e)
        {
            SelectedProd = (Base.Product)prodList.SelectedItem;
            if (MessageBox.Show("Удалить запись?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                try
                {
                    SourceCore.MyBase.Product.Remove((Base.Product)prodList.SelectedItem);
                    SourceCore.MyBase.SaveChanges();
                    UpdateList(null);
                }
                catch
                {
                    MessageBox.Show("Вы не можете удалить используемый товар!");
                }
            }
        }

        private void redactbtn_Click(object sender, RoutedEventArgs e)
        {
            SelectedProd = (Base.Product)prodList.SelectedItem;
            redact newWindow = new redact(SelectedProd, this);
            newWindow.ShowDialog();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            LOGIN newLogin = new LOGIN();
            newLogin.Show();
            Close();
        }

        private void filterText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            prodList.ItemsSource = SourceCore.MyBase.Product.Where(Q => Q.ProductName.Contains(textbox.Text) || 
            Q.ProductDescription.Contains(textbox.Text) ||
            Q.ProductManufacturer.Contains(textbox.Text) ||
            Q.ProductCategory.Contains(textbox.Text)
            ).ToList();
        }

        private void sort_Click(object sender, RoutedEventArgs e)
        {
            if (sortOrder)
            {
                prodList.Items.SortDescriptions.Clear();
                prodList.Items.SortDescriptions.Add(new SortDescription("ProductCost",
                    ListSortDirection.Ascending));
                sortOrder = false;
            }
            else
            {
                prodList.Items.SortDescriptions.Clear();
                prodList.Items.SortDescriptions.Add(new SortDescription("ProductCost",
                    ListSortDirection.Descending));
                sortOrder = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            combo.Text = "";
        }
    }
}
