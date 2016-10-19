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
using FoodDiary_Backend.Models;
using FoodDiary_Backend.Repositories;
using MaterialDesignThemes.Wpf;

namespace MaterialDesign.DesktopUI.DinamicItems
{
    /// <summary>
    /// Логика взаимодействия для ProductCard.xaml
    /// </summary>
    public partial class ProductCard : UserControl
    {
        private Product _product;

        private int _numberOfGrams;

        private DateTime _date;

        private Today.ReloadCards _reload;

        public ProductCard(Product product, int number, DateTime date, Today.ReloadCards reload)
        {
            InitializeComponent();
            _product = product;
            _numberOfGrams = number;
            _date = date;
            OutputValuesOnCard();
            _reload = reload;
        }

        private void OutputValuesOnCard()
        {
            txbProductName.Text = _product.Name;
            txbCalories.Text += " "+_product.CaloriesIn100G;
            txbFats.Text += " " + _product.FatIn100G;
            txbProteins.Text += " " + _product.ProteinIn100G;
            txbCategory.Text += " " + _product.CategoryName;
            txbSubcat.Text += " " + _product.SubcategoryName;
            txbCarbohudrates.Text += " " + _product.CarbohydratesIn100G;
            txbNumber.Text = " " + _numberOfGrams.ToString();
            txbNumberCal.Text = Math.Round(_product.CaloriesIn100G/100*_numberOfGrams, 2).ToString();
        }

        private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow.UserNutritionRepository.DropProductInNutrition(MainWindow.UserId, _date, _product.Name);
            _reload();
        }

        private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
        {
            var view = new EditDialog(_product,_numberOfGrams, _date);
            DialogHost.Show(view, "RootDialog", ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter.Equals(true))
            {
                _reload();
            }
        }
    }
}
