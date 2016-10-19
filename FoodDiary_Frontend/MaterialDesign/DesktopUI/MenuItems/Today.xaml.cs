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
        private IList<UserNutrition> _currentUserNutrition;
        private static DateTime _dateToLoad;

        public delegate void ReloadCards();

        private ReloadCards _reload;

        public Today()
        {
            InitializeComponent();

            _dateToLoad =DateTime.Now;
            _reload += () => LoadCards();
            LoadCards();
        }

        public void LoadCards()
        {
            stpCards.Children.Clear();

            txbCurDate.Text = _dateToLoad.ToLongDateString();

            //date to find products
            _currentUserNutrition = MainWindow.UserNutritionRepository.GetAllUserProductsByDate(MainWindow.UserId, _dateToLoad);
            if (_currentUserNutrition.Count() > 0)
            {
                foreach (var i in _currentUserNutrition)
                {
                    stpCards.Children.Add(new ProductCard(i.Product, i.NumberOfGrams,_dateToLoad,_reload));
                }
            }
            else
            {
                ErrorDialog messageToAddProd = new ErrorDialog("There are no added products in this day.");
                messageToAddProd.Show();
            }
        }
        
        private void AddDialog(object sender, RoutedEventArgs e)
        {
            var view = new SampleDialog(_dateToLoad);
            DialogHost.Show(view, "RootDialog", ClosingEventHandler);
            
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter.Equals(true))
            {
                LoadCards();
            }
        }

        private void PickDate(object sender, RoutedEventArgs e)
        {
            dtCalendar.IsDropDownOpen = true;
        }

        private void DateChanged(object sender, SelectionChangedEventArgs e)
        {
            _dateToLoad = dtCalendar.SelectedDate.Value;
            LoadCards();
        }

    }
}
