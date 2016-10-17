using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
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
using MaterialDesign;
using MaterialDesignThemes.Wpf;
using static MaterialDesignThemes.Wpf.DialogHost;


namespace FoodDiary
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private DbContext context;
        private int userId;

        public LoginWindow()
        {
            InitializeComponent();
        }

        public LoginWindow(DbContext context, int userId)
        {
            this.context = context;
            this.userId = userId;
        }

        private void btnSubmit_OnClick(object sender, RoutedEventArgs e)
        {
            if (txbLogin.Text == "" || txbPass.Password == "")
            {
                ErrorDialog errorDialog=new ErrorDialog("Error!!! Fill all the fields!!!");
                errorDialog.Show();
            }
            else
            {
                DbConnectionFactory connectionFactory=new DbConnectionFactory("FoodDiaryConnectionString");
                DbContext context = new DbContext(connectionFactory);
                UserRepository userRepository=new UserRepository(context);
                string hasedPassword = HashedPassword.GetMd5Hash(txbPass.Password);
                int userId = userRepository.GetUserByLogin(txbLogin.Text, hasedPassword);
                if (userId > 0)
                {
                    MainWindow userMainWindow = new MainWindow(context, userId);
                    userMainWindow.Show();
                    Hide();
                }
                else
                {
                    ErrorDialog errorDialog = new ErrorDialog("Error!!! Incorrect data inputed!!!");
                    errorDialog.Show();
                }
               
            }
        }
        private void ClosingEventHandler(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("You can intercept the closing event, and cancel here.");
        }
    }
}
