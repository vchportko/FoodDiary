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
using FoodDiary_Backend.Models;
using FoodDiary_Backend.Repositories;
using FoodDiary_Backend.Repositories.Interfaces;

namespace MaterialDesign
{
    /// <summary>
    /// Interaction logic for SampleDialog.xaml
    /// </summary>
    public partial class EditDialog : UserControl
    {
        private Product _currentProduct ;

        private int _currentNumber;

        private string _currentSubcategory;

        private DateTime _currentDate;

        private bool _ifProductWasChanged;

        public EditDialog(Product product, int number, DateTime date)
        {
            InitializeComponent();
            _currentProduct = product;
            _currentNumber = number;
            _currentDate = date;
            _currentSubcategory = product.SubcategoryName;
            _ifProductWasChanged = false;
            OutputCurrentProduct();
        }

        private void OutputCurrentProduct()
        {
            List<Category> categories=MainWindow.CategoryRepository.GetAllCategories("");

            int selectedindex=0;
            //add items to combobox
            foreach (var i in categories)
            {
                cbCategory.Items.Add(i.Name);
                if (i.Name == _currentProduct.CategoryName)
                {
                    selectedindex = categories.IndexOf(i);
                }
            }

            //set current category
            cbCategory.SelectedIndex = selectedindex;

            tbCalories.Text = _currentNumber.ToString();

        }

        //setting subcategory combobox items
        private void setSubcategoryItems(string categoryName)
        {
            int selectedindex = 0;
            List<Subcategory> currentSubcategoryList =
                MainWindow.SubcategoryRepository.GatAllSubcategories(categoryName, "");

            cbSubcategory.Items.Clear();
            foreach (var i in currentSubcategoryList)
            {
                cbSubcategory.Items.Add(i.Name);
                if (i.Name == _currentProduct.SubcategoryName)
                {
                    selectedindex = currentSubcategoryList.IndexOf(i);
                }
            }

            //set current subcategory
            cbSubcategory.SelectedIndex = selectedindex;
        }

        //setting product combobox items
        private void setProductItems(string subcategoryName)
        {
            int selectedindex = 0;
            List<Product> currentProductsList =MainWindow.ProductRepository.GetAllProducts(subcategoryName,"");

            cbProductName.Items.Clear();
            foreach (var i in currentProductsList)
            {
                cbProductName.Items.Add(i.Name);
                if (i.Name == _currentProduct.Name)
                {
                    selectedindex = currentProductsList.IndexOf(i);
                }
            }

            //set current subcategory
            cbProductName.SelectedIndex = selectedindex;
        }

        private void UpdateFood(object sender, RoutedEventArgs e)
        {
            bool result = false;
            if (_ifProductWasChanged)
            {
                result = MainWindow.UserNutritionRepository.UpdateProductInNutrition(MainWindow.UserId,
                    _currentDate,
                    _currentSubcategory, _currentProduct.Name, _currentNumber);
                if (result)
                {
                    ErrorDialog success = new ErrorDialog("Data was commited.");
                    success.Show();
                }
                else
                {
                    ErrorDialog errorCommiting = new ErrorDialog("Data was not commited.");
                    errorCommiting.Show();
                }
            }
            else
            {
                ErrorDialog noChanges=new ErrorDialog("No changes made.");
                noChanges.Show();
            }
        }

        private void CbCategory_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            setSubcategoryItems(e.AddedItems[0].ToString());
        }

        private void CbSubcategory_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            setProductItems(e.AddedItems[0].ToString());

            //change currentSubcategory
            if (e.AddedItems != null)
            {
                _currentSubcategory = e.AddedItems[0].ToString();
            }
            else
            {
                _currentSubcategory = "";
            }
            _ifProductWasChanged = true;
        }

        private void CbProductName_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ifProductWasChanged = true;
            if (e.AddedItems != null)
            {
                _currentProduct.Name = e.AddedItems[0].ToString();
            }
            else
            {
                _currentProduct.Name = "";
            }
        }

        private void TbCalories_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _ifProductWasChanged = true;
            _currentNumber = int.Parse(tbCalories.Text);
        }

        private void TbCalories_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
    }
}
