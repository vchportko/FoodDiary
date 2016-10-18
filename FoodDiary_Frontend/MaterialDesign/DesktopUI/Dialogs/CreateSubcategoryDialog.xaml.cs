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
    public partial class CreateSubcategoryDialog : UserControl
    {
        private string _currentCategory;

        private string _curentSubcategory;

        public CreateSubcategoryDialog()
        {
            InitializeComponent();
            LoadCategories();
            _currentCategory = "";
            _curentSubcategory = "";
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

        
        private void CreateSubcategoryDB(object sender, RoutedEventArgs e)
        {
            _curentSubcategory = tbSubcategory.Text;
            if (_curentSubcategory != "")
            {
                if (MainWindow.SubcategoryRepository.AddNewSubcategory(_currentCategory, _curentSubcategory))
                {
                    ErrorDialog success=new ErrorDialog("Subcategory added.");
                    success.Show();
                }
                else
                {
                    ErrorDialog errorAddingSubcat = new ErrorDialog("Error!!! Subcategory was not added.");
                    errorAddingSubcat.Show();
                }
            }
        }

        private void CbCategory_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _currentCategory = e.AddedItems[0].ToString();
        }
    }
}
