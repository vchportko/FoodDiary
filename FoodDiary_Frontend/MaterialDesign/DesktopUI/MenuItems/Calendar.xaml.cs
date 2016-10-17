using MaterialDesignThemes.Wpf;
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

namespace MaterialDesign
{
    /// <summary>
    /// Interaction logic for Calendar.xaml
    /// </summary>
    public partial class Calendar : UserControl
    {
        public Calendar()
        {
            InitializeComponent();
        }

        private void EditDialog(object sender, RoutedEventArgs e)
        {
            // pass data of selected
            var view = new EditDialog { };

            DialogHost.Show(view, "RootDialog", ClosingEventHandler);
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            // delete fom DB and refresh

        }

        private void PickDate(object sender, RoutedEventArgs e)
        {
            dtCalendar.IsDropDownOpen = true;
        }

        private void DateChanged(object sender, SelectionChangedEventArgs e)
        {
            var date = dtCalendar.SelectedDate.Value;
            Console.WriteLine(date);
        }

        private void ClosingEventHandler(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("You can intercept the closing event, and cancel here.");
        }
    }
}
