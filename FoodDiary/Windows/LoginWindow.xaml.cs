using System;
using System.Collections.Generic;
using System.Data;
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
using FoodDiary_Backend;
using FoodDiary_Backend.DataAccess;
using FoodDiary_Backend.Repositories;
using FoodDiary_Backend.Services;


namespace FoodDiary
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSubmit_OnClick(object sender, RoutedEventArgs e)
        {
            if (txbLogin.Text == "" || txbPass.Text == "")
            {
                
            }
            else
            {
                DbConnectionFactory connectionFactory=new DbConnectionFactory("FoodDiaryConnectionString");
                DbContext context = new DbContext(connectionFactory);
                UserRepository userRepository=new UserRepository(context);
                string hasedPassword = HashedPassword.GetMd5Hash(txbPass.Text);
                int userId = userRepository.GetUserByLogin(txbLogin.Text, hasedPassword);
            }
        }
    }
}
