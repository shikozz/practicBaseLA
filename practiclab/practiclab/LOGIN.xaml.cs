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
using System.Windows.Threading;

namespace practiclab
{
    /// <summary>
    /// Interaction logic for LOGIN.xaml
    /// </summary>
    public partial class LOGIN : Window
    {

        private Base.practic_LAEntities DataBase;
        private DispatcherTimer dispatcherTimer;
        private bool needCaptcha = false;
        private int sec = 0;

        public LOGIN()
        {
            InitializeComponent();
            loginbtn.Background = new SolidColorBrush(Color.FromRgb(118, 227, 131));
            guestbtn.Background = new SolidColorBrush(Color.FromRgb(118, 227, 131));
            captchabtn.Background = new SolidColorBrush(Color.FromRgb(118, 227, 131));
            dispatcherTimer = new DispatcherTimer();

            try
            {
                DataBase = new Base.practic_LAEntities();
            }
            catch
            {
                MessageBox.Show("Не удалось подключиться к базе данных. Проверьте настройки подключения приложения.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                Close();
            }
        }

        private void Autho()
        {
            captchaInput.Text = " " + captchaInput.Text;
            Base.User User = DataBase.User.SingleOrDefault(U => U.UserLogin == logintxt.Text && U.UserPassword == passtxt.Text);
            if (User !=null && captchatxt.Text==captchaInput.Text)
            {
                MainWindow window = new MainWindow();
                window.Show();
                Close();
            }
            else
            {

                loginbtn.IsEnabled = false;
                guestbtn.IsEnabled= false;
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
                dispatcherTimer.Start();
                captchab.Visibility = Visibility;
                captchat.Visibility = Visibility;
                captchai.Visibility = Visibility;
                generateCaptcha();
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            loginbtn.IsEnabled = true;
            guestbtn.IsEnabled = true;
            dispatcherTimer.Stop();
        }

        private void generateCaptcha()
        {
            String allowchar = " ";
            allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
            allowchar += "1,2,3,4,5,6,7,8,9,0,11,12,13,14,15,16,17,18,19,10";

            char[] a = { ',' };

            String[] ar = allowchar.Split(a);
            String pwd = " ";
            string temp = " ";
            Random r = new Random();
            for (int i = 0; i < 8; i++)
            {
                temp = ar[(r.Next(0, ar.Length))];
                pwd += temp;
            }
            captchaInput.Text = "";
            captchatxt.Text = pwd;
        }

        private void loginbtn_Click(object sender, RoutedEventArgs e)
        {
            Autho();
        }

        private void captchabtn_Click(object sender, RoutedEventArgs e)
        {
            if(captchaInput.Text == captchatxt.Text)
            {
                needCaptcha = false;
            }
        }
    }
}
