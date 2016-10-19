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
    public partial class SampleDialog : UserControl
    {
        private bool _ifWasFieldsChanged;

        private DateTime _currentDate;

        private string _currentSubcategory;

        private string _currentProductName;

        private int _currentNumber;

        public SampleDialog(DateTime date)
        {
            InitializeComponent();
            _currentDate = date;
            _currentSubcategory = "";
            _currentProductName = "";
            _currentNumber = 0;
            _ifWasFieldsChanged = false;
            LoadCategories();
        }

        void LoadCategories()
        {
            List<Category> categories = MainWindow.CategoryRepository.GetAllCategories("");

            //add items to combobox
            foreach (var i in categories)
            {
                cbCategory.Items.Add(i.Name);
            }
        }
        
        private void AddProductToUserNutrition(object sender, RoutedEventArgs e)
        {
            if (_ifWasFieldsChanged)
            {
                if (cbCategory.SelectedIndex != -1 && cbSubcategory.SelectedIndex != -1 &&
                    cbProductName.SelectedIndex != -1 && tbCalories.Text != "")
                {
                    if (MainWindow.UserNutritionRepository.AddNewProductToNutrition(MainWindow.UserId, _currentDate,
                        _currentProductName, _currentNumber) > 0)
                    {
                        ErrorDialog success = new ErrorDialog("Data was commited.");
                        success.Show();
                    }
                    else
                    {
                        ErrorDialog errorCommiting = new ErrorDialog("Error!!! Data was not commited.");
                        errorCommiting.Show();
                    }
                }
                else
                {
                    ErrorDialog errorCommiting = new ErrorDialog("Error!!! Data was not commited. Not all fields were filled.");
                    errorCommiting.Show();
                }
            }

        }

        private void setSubcategoryItems(string categoryName)
        {
            _ifWasFieldsChanged = true;

            int selectedindex = 0;
            List<Subcategory> currentSubcategoryList =
                MainWindow.SubcategoryRepository.GatAllSubcategories(categoryName, "");

            cbSubcategory.Items.Clear();
            foreach (var i in currentSubcategoryList)
            {
                cbSubcategory.Items.Add(i.Name);
            }

            //set current subcategory
            cbSubcategory.SelectedIndex = selectedindex;
        }

        //setting product combobox items
        private void setProductItems(string subcategoryName)
        {
            _ifWasFieldsChanged = true;

            int selectedindex = 0;
            List<Product> currentProductsList = MainWindow.ProductRepository.GetAllProducts(subcategoryName, "");

            cbProductName.Items.Clear();
            foreach (var i in currentProductsList)
            {
                cbProductName.Items.Add(i.Name);
            }

            //set current subcategory
            cbProductName.SelectedIndex = selectedindex;
        }

        private void CbCategory_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ifWasFieldsChanged = true;

            setSubcategoryItems(e.AddedItems[0].ToString());
        }

        private void CbSubcategory_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ifWasFieldsChanged = true;

            if (e.AddedItems.Count != 0)
            {
                setProductItems(e.AddedItems[0].ToString());
                _currentSubcategory = e.AddedItems[0].ToString();
            }
            else
            {
                _currentSubcategory = "";
            }
        }

        private void CbProductName_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ifWasFieldsChanged = true;

            if (e.AddedItems.Count != 0)
            {
                _currentProductName = e.AddedItems[0].ToString();
            }
            else
            {
                _currentProductName = "";
            }
        }

        private void TbCalories_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _ifWasFieldsChanged = true;

            if (tbCalories.Text != "")
            {
                _currentNumber = int.Parse(tbCalories.Text);
            }
            
        }

        private void TbCalories_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
    }
}
