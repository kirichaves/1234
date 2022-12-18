﻿using Microsoft.EntityFrameworkCore;
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
using WpfApp10;
using Ykrahenia.Infrastucture;
using Ykrahenia.Models;

namespace Ykrahenia
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        bool verify = true;
        int verifyCheck = 0;
        public LoginWindow()
        {
            InitializeComponent();

            captchaBlock.Visibility = Visibility.Collapsed;
            captchaBox.Visibility = Visibility.Collapsed;
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            using (TradeContext db = new TradeContext())
            {

                // проверка, если есть каптча
                if (captchaBlock.Visibility == Visibility.Visible)
                {
                    if (captchaBlock.Text == captchaBox.Text)
                    {
                        verify = true;
                    }
                }

                User user = db.Users.Where(u => u.UserLogin == loginBox.Text && u.UserPassword == passwordBox.Password).Include(u => u.UserRoleNavigation).FirstOrDefault() as User;

                // admin
                if (user != null && verify)
                {
                    new MainWindow(user).Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неуспешная авторизация");
                    verifyCheck += 1;

                    // captcha view
                    captchaBox.Visibility = Visibility.Visible;
                    captchaBlock.Visibility = Visibility.Visible;
                    captchaBlock.Text = CaptchaBuilder.Refresh();
                    verify = false;

                    if (verifyCheck > 1)
                    {
                        disableButton();
                        captchaBlock.Text = CaptchaBuilder.Refresh();
                    }
                }
            }
        }


        /// <summary>
        /// Асинхронное выключение кнопки на 10 сек.
        /// </summary>
        async void disableButton()
        {
            loginButton.IsEnabled = false;
            await Task.Delay(TimeSpan.FromSeconds(10));
            loginButton.IsEnabled = true;
        }

        private void guestButtonClick(object sender, RoutedEventArgs e)
        {
            new MainWindow(null).Show();
            this.Close();
        }
    }
}
