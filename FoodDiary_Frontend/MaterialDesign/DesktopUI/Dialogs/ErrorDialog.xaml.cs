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
using FoodDiary_Backend.DataAccess;

namespace MaterialDesign
{
    /// <summary>
    /// Interaction logic for SampleDialog.xaml
    /// </summary>
    public partial class ErrorDialog:Window
    {
        public ErrorDialog(string message)
        {
            InitializeComponent();
            txbErrorMessage.Text = message;
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        
    }
}
