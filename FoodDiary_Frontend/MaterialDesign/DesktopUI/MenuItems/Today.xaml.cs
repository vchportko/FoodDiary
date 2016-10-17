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
using FoodDiary_Backend.Models;
using FoodDiary_Backend.Repositories;
using MaterialDesign.DesktopUI.DinamicItems;

namespace MaterialDesign
{
    /// <summary>
    /// Interaction logic for Today.xaml
    /// </summary>
    public partial class Today : UserControl
    {
        private DbContext _dbContext;
        private int _userId;
        private UserNutritionRepository _currentUserNutritionRepository;
        private IList<UserNutrition> _currentUserNutrition;
        private DateTime _dateToLoad;

        public Today()
        {
            InitializeComponent();
            _dbContext = MainWindow._context;
            _userId = MainWindow._userId;
            _currentUserNutritionRepository=new UserNutritionRepository(_dbContext);
            _dateToLoad=DateTime.Now;
            LoadCards();
            txbCurDate.Content = _dateToLoad.ToLongDateString();
        }

        private void LoadCards()
        {
            //date to find products
            _currentUserNutrition = _currentUserNutritionRepository.GetAllUserProductsByDate(_userId, _dateToLoad);
            if (_currentUserNutrition.Count() > 0)
            {
                foreach (var i in _currentUserNutrition)
                {
                    stpCards.Children.Add(new ProductCard(i.Product, i.NumberOfGrams));
                }
            }
            else
            {
                ErrorDialog messageToAddProd = new ErrorDialog("There are no added products in this day.");
            }
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

        private void PickDate(object sender, RoutedEventArgs e)
        {
            dtCalendar.IsDropDownOpen = true;
        }

        private void DateChanged(object sender, SelectionChangedEventArgs e)
        {
            var date = dtCalendar.SelectedDate.Value;
            Console.WriteLine(date);
        }

    }
}
