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

namespace MaterialDesign
{
    /// <summary>
    /// Interaction logic for SampleDialog.xaml
    /// </summary>
    public partial class CreateFoodDialog : UserControl
    {
        private string _currentSubcategory;

        private string _currentCategory;

        private string _currentProductName;

        private float _calories;

        private float _fats;

        private float _proteins;

        private float _carbo;

        public CreateFoodDialog()
        {
            InitializeComponent();
            _currentCategory = "";
            _currentSubcategory = "";
            _currentProductName = "";
            _calories = 0;
            _fats = 0;
            _proteins = 0;
            _carbo = 0;
            LoadCategories();

        }

        private void LoadCategories()
        {
            List<Category> categories = MainWindow.CategoryRepository.GetAllCategories("");

            //add items to combobox
            foreach (var i in categories)
            {
                cbCategory.Items.Add(i.Name);
            }
        }

        private void setSubcategoryItems(string categoryName)
        {
            List<Subcategory> currentSubcategoryList =
                MainWindow.SubcategoryRepository.GatAllSubcategories(categoryName, "");

            cbSubcategory.Items.Clear();
            foreach (var i in currentSubcategoryList)
            {
                cbSubcategory.Items.Add(i.Name);
            }
        }

        private void CreateFoodDB(object sender, RoutedEventArgs e)
        {
            _currentSubcategory = cbSubcategory.Text;
            if (tbCalories.Text!="")
            {
                _calories = float.Parse(tbCalories.Text);
            }
            if (tbFat.Text != "")
            {
                _fats = float.Parse(tbFat.Text);
            }
            if (tbProteins.Text != "")
            {
                _proteins = float.Parse(tbProteins.Text);
            }
            if (tbCarbohydrates.Text != "")
            {
                _carbo = float.Parse(tbCarbohydrates.Text);
            }
            _currentProductName = tbFood.Text;
            if (_currentCategory != ""&&_currentSubcategory!="" && _currentProductName != "" && _calories != 0)
            {
                if (MainWindow.ProductRepository.AddNewProduct(_currentProductName, _currentSubcategory, _calories,
                    _fats, _proteins, _carbo))
                {
                    ErrorDialog success = new ErrorDialog("Product was added.");
                    success.Show();
                }
                else
                {
                    ErrorDialog errorAddingProduct = new ErrorDialog("Error!!! Product was not added.");
                    errorAddingProduct.Show();
                }
            }
            else
            {
                ErrorDialog errorAddingProduct = new ErrorDialog("Error!!! Product was not added.");
                errorAddingProduct.Show();
            }
        }

        private void CbCategory_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedItem = e.AddedItems[0].ToString();
            setSubcategoryItems(selectedItem);
            _currentCategory = selectedItem;
        }

        private void TextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
    }
}
