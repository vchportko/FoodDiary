using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FoodDiary_Backend.DataAccess;
using FoodDiary_Backend.Models;
using FoodDiary_Backend.Repositories;
using FoodDiary_Backend.Repositories.Interfaces;

namespace MaterialDesign
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int UserId;

        public static CategoryRepository CategoryRepository { get; private set; }

        public static SubcategoryRepository SubcategoryRepository { get; private set; }

        public static ProductRepository ProductRepository { get; private set; }

        public static UserNutritionRepository UserNutritionRepository { get; private set; }

        public MainWindow(DbContext context, int userId)
        {
            UserId = userId;
            //initializing repositories
            CategoryRepository = new CategoryRepository(context);
            SubcategoryRepository = new SubcategoryRepository(context);
            ProductRepository = new ProductRepository(context);
            UserNutritionRepository = new UserNutritionRepository(context);

            InitializeComponent();
        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //until we had a StaysOpen glag to Drawer, this will help with scroll bars
            var dependencyObject = Mouse.Captured as DependencyObject;
            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            MenuToggleButton.IsChecked = false;
        }

        private void Today_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
