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

namespace TradeApp.View
{
    /// <summary>
    /// Логика взаимодействия для GuestPage.xaml
    /// </summary>
    public partial class GuestPage : Window
    {
        private Database.TradeEntities tradeEntities;
        public GuestPage(Database.TradeEntities tradeEntities)
        {
            this.tradeEntities = tradeEntities;
            InitializeComponent();
        }

        private void Submit_Exit(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            Hide();
        }
    }
}
