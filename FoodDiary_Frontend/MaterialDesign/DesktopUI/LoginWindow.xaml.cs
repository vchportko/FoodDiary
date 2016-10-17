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
using MaterialDesign;
using MaterialDesign.DesktopUI.Dialogs;
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
                ErrorDialog errorDialog=new ErrorDialog("Input all fields");
#pragma warning disable 4014
                DialogHost.Show(errorDialog, "RootDialog");
#pragma warning restore 4014
            }
            else
            {
                DbConnectionFactory connectionFactory=new DbConnectionFactory("FoodDiaryConnectionString");
                DbContext context = new DbContext(connectionFactory);
                UserRepository userRepository=new UserRepository(context);
                string hasedPassword = HashedPassword.GetMd5Hash(txbPass.Password);
                int userId = userRepository.GetUserByLogin(txbLogin.Text, hasedPassword);
                MainWindow userMainWindow=new MainWindow(context,userId);
                userMainWindow.Show();
                Hide();
            }
        }
    }
}
