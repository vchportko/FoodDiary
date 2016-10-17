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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void CreateCategory(object sender, RoutedEventArgs e)
        {
            var view = new CreateCategoryDialog { };

            DialogHost.Show(view, "RootDialog", ClosingEventHandler);
        }

        private void CreateSubcategory(object sender, RoutedEventArgs e)
        {
            var view = new CreateSubcategoryDialog { };

            DialogHost.Show(view, "RootDialog", ClosingEventHandler);
        }

        private void CreateFood(object sender, RoutedEventArgs e)
        {
            var view = new CreateFoodDialog { };

            DialogHost.Show(view, "RootDialog", ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("You can intercept the closing event, and cancel here.");
        }
    }
}
