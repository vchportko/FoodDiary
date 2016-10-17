using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for Today.xaml
    /// </summary>
    public partial class Today : UserControl
    {
        DbContext _dbContext;
        int _userId;
        public Today()
        {
            InitializeComponent();
            _dbContext = MainWindow._context;
            _userId = MainWindow._userId;
        }

        
        private void AddDialog(object sender, RoutedEventArgs e)
        {
            var view = new SampleDialog { };

            DialogHost.Show(view, "RootDialog", ClosingEventHandler);
        }

        private void EditDialog(object sender, RoutedEventArgs e)
        {
            // pass data of selected
            var view = new EditDialog ();
          
            DialogHost.Show(view, "RootDialog", ClosingEventHandler);
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            // delete fom DB and refresh
            
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("You can intercept the closing event, and cancel here.");
        }

    }
}
