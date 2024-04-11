using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace TradeApp.View
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        private readonly Database.TradeEntities TradeEntities;
        private Database.User user;
        private GuestPage guestPage;
        private SuppliesPage suppliesPage;
        public Random rand;
        private string captchaSymbols = "QWERTYUIOPASDFGHJKLZXCVBNM1234567890";
        private bool captchaEnabled = false;
        private string captchaCode;
        private const double COLOR_MULTIPLIER = 0.5;
        public Auth()
        {
            rand = new Random(Environment.TickCount);
            TradeEntities = new Database.TradeEntities();
            InitializeComponent();
        }

        private void Submit_Login(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text.Trim();
            string pass = tbPass.Password.Trim();

            if (captchaEnabled && tbCaptcha.Text.ToUpper() != captchaCode)
            {
                generateCaptcha();
                MessageBox.Show("Введите капчу");
                return;
            }

            if(login.Length < 1 || pass.Length < 1) 
            {
                generateCaptcha();
                MessageBox.Show("Введите логин и пароль");
                return; 
            }

            user = TradeEntities.User.Where(u => u.Login == login && u.Password == pass).FirstOrDefault();

            if (user == null)
            {
                generateCaptcha();
                MessageBox.Show("Неверный логин/пароль");
                return;
            }

            MessageBox.Show("Ваше ФИО: "+user.Surname + " " + user.Name + " " + user.Patronymic);

            tbLogin.Clear();
            tbPass.Clear();
            captchaEnabled = false;
            CaptchaBorder.Visibility = Visibility.Collapsed;

            suppliesPage = new SuppliesPage(TradeEntities, user);
            suppliesPage.Owner = this;
            suppliesPage.Show();
            Hide();

        }

        private void Submit_Guest(object sender, RoutedEventArgs e)
        {
            guestPage = new GuestPage(TradeEntities);
            guestPage.Owner = this;
            guestPage.Show();
            Hide();
        }

        private void generateCaptcha()
        {
            CaptchaCanv.Children.Clear();
            if (!captchaEnabled) 
            {
                captchaEnabled = true;
                CaptchaBorder.Visibility = Visibility.Visible;
            }
            captchaCode = getNewCaptchaCode();
            for (int i = 0; i < captchaCode.Length; i++)
            {
                addCharToCanvas(i, captchaCode[i]);
            }
            for (int i = 0;i < 1000; i++) 
            {
                addNoise();
            }
            for(int i = 0; i < 15; i++)
            {
                addLine();
            }
        }

        private void addNoise()
        {
            Ellipse ellipse = new Ellipse()
            {
                Width = rand.Next(2, 5),
                Height = rand.Next(2, 5),
                Fill = getRandColor(COLOR_MULTIPLIER),
                Stroke = getRandColor(COLOR_MULTIPLIER)
            };
            double x = rand.NextDouble() * (GetWindow(AuthWindow).ActualWidth - WindowBorder.Padding.Left * 3.5);
            double y = rand.NextDouble() * CaptchaCanv.Height;

            CaptchaCanv.Children.Add(ellipse);

            Canvas.SetLeft(ellipse, x);
            Canvas.SetTop(ellipse, y);
        }
        private void addLine()
        {
            double x1;
            double y1;
            double x2;
            double y2;
            int firstPoint = rand.Next(2);
            if (firstPoint == 1) 
            {
                x1 = rand.NextDouble() * (GetWindow(AuthWindow).ActualWidth - WindowBorder.Padding.Left * 3.5);
                y1 = 0;
            }
            else
            {
                x1 = 0;
                y1 = rand.NextDouble() * CaptchaCanv.Height;
            }
            int secondPoint = rand.Next(2);
            if (secondPoint == 1)
            {
                x2 = rand.NextDouble() * (GetWindow(AuthWindow).ActualWidth - WindowBorder.Padding.Left * 3.5);
                y2 = CaptchaCanv.Height;
            }
            else
            {
                x2 = GetWindow(AuthWindow).ActualWidth - WindowBorder.Padding.Left * 3.5;
                y2 = rand.NextDouble() * CaptchaCanv.Height;
            }
            Line line = new Line()
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Stroke = getRandColor(COLOR_MULTIPLIER),
                StrokeThickness = 2
            };
            CaptchaCanv.Children.Add(line);
        }
        

        private void addCharToCanvas(int index, char ch)
        {
            Label label = new Label();
            label.Content = ch;
            label.FontWeight = FontWeights.Bold;
            label.Height = 80;
            label.Width = 80;
            label.FontSize = rand.Next(32, 48);
            label.Foreground = getRandColor(COLOR_MULTIPLIER);
            label.HorizontalContentAlignment = HorizontalAlignment.Center;  
            label.VerticalContentAlignment = VerticalAlignment.Center;
            label.RenderTransformOrigin = new Point(0.5, 0.5);
            label.RenderTransform = new RotateTransform(rand.Next(-20, 20));

            CaptchaCanv.Children.Add(label);

            double startPos = tbLogin.ActualWidth/2 - (label.Width * 4 / 3.5);
            Canvas.SetLeft(label, startPos + 32 * index);
            Canvas.SetTop(label, 6);


        }

        private string getNewCaptchaCode()
        {
            string capt = "";
            for (int i = 0; i < 4; i++)
            {
                capt += captchaSymbols[rand.Next(captchaSymbols.Length - 1)];
            }
            return capt;
        }

        private SolidColorBrush getRandColor(double multiplier) {
            return new SolidColorBrush(Color.FromScRgb(1, (float)(rand.NextDouble() * multiplier), (float)(rand.NextDouble() * multiplier), (float)(rand.NextDouble() * multiplier)));
        }
    }
}
