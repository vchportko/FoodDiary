using De.TorstenMandelkow.MetroChart;
using MaterialDesign.DesktopUI.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : UserControl
    {
        public ObservableCollection<GraphicPoint> Calories { get; private set; }

        public ObservableCollection<GraphicPoint> Fat { get; private set; }

        public ObservableCollection<GraphicPoint> Protein { get; private set; }

        public ObservableCollection<GraphicPoint> Carbohydrate { get; private set; }

        public Statistics()
        {
            InitializeComponent();
            DataContext = this;
            Calories=new ObservableCollection<GraphicPoint>();
            Fat = new ObservableCollection<GraphicPoint>();
            Protein = new ObservableCollection<GraphicPoint>();
            Carbohydrate = new ObservableCollection<GraphicPoint>();
            LoadWeekCalories();
            LoadWeekFat();
            LoadWeekProtein();
            LoadWeekCarbohydrate();
        }

        private void LoadWeekCalories()
        {
            var calories = MainWindow.UserNutritionRepository.GetSumsOfCalories(MainWindow.UserId, 7);
            foreach (var i in calories)
            {
                Calories.Add(new GraphicPoint() {Date=i.Key.ToShortDateString(),Number=i.Value});
            }
        }

        private void LoadWeekFat()
        {
            var fat = MainWindow.UserNutritionRepository.GetSumsOfFat(MainWindow.UserId, 7);
            foreach (var i in fat)
            {
                Fat.Add(new GraphicPoint() { Date = i.Key.ToShortDateString(), Number = i.Value });
            }
        }

        private void LoadWeekProtein()
        {
            var protein = MainWindow.UserNutritionRepository.GetSumsOfProtein(MainWindow.UserId, 7);
            foreach (var i in protein)
            {
                Protein.Add(new GraphicPoint() { Date = i.Key.ToShortDateString(), Number = i.Value });
            }
        }

        private void LoadWeekCarbohydrate()
        {
            var carbohydrate = MainWindow.UserNutritionRepository.GetSumsOfCarbohydrates(MainWindow.UserId, 7);
            foreach (var i in carbohydrate)
            {
                Carbohydrate.Add(new GraphicPoint() { Date = i.Key.ToShortDateString(), Number = i.Value });
            }
        }

        private object selectedItem = null;
        public object SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                // selected item has changed
                selectedItem = value;
            }
        }


    }
}
