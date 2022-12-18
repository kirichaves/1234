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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ykrahenia.Models;

namespace WpfApp10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User? currentUser = null;
        public MainWindow(User user)
        {
            InitializeComponent();
            using (TradeContext db = new TradeContext())
            {
                if (user != null)
                {
                    currentUser = user;
                    statusUser.Text = $"{user.UserRoleNavigation.RoleName}: {user.UserSurname} {user.UserName} {user.UserPatronymic}. \r\t";
                    //MessageBox.Show($"{user.RoleNavigation.Name}: {user.Surname} {user.Name} {user.Patronymic}. \r\t");




                    if (currentUser == null || currentUser.UserRoleNavigation.RoleName != "Администратор")
                    {
                        addProduct.Visibility = Visibility.Collapsed;
                        deleteProduct.Visibility = Visibility.Collapsed;
                    }

                }
                else
                {
                    statusUser.Text = "Гость";
                    //MessageBox.Show("Гость");
                }
            }







        }
    }
}
