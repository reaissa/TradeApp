using Database;
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
using System.Xml.Linq;

namespace TradeApp.View
{
    public partial class SuppliesPage : Window
    {
        private Database.TradeEntities tradeEntities;
        private Database.User user; 
        public SuppliesPage(Database.TradeEntities tradeEntities, User user)
        {
            this.tradeEntities = tradeEntities;
            this.user = user;
            InitializeComponent();
            lName.Content = "ФИО: " + user.Surname + " " + user.Name + " " + user.Patronymic;
        }

        private void Submit_Exit(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            Hide();
        }
    }
}
