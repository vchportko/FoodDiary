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
    /// Interaction logic for SampleDialog.xaml
    /// </summary>
    public partial class CreateCategoryDialog : UserControl
    {
        private string _categoryName;

        public CreateCategoryDialog()
        {
            InitializeComponent();
            _categoryName = "";
        }

        
        private void CreateCategoryDB(object sender, RoutedEventArgs e)
        {
            _categoryName = tbCategory.Text;
            if (_categoryName != "")
            {
                if (MainWindow.CategoryRepository.AddNewCategory(_categoryName))
                {
                    ErrorDialog success = new ErrorDialog("Category added.");
                    success.Show();
                }
                else
                {
                    ErrorDialog error = new ErrorDialog("Error!!! Category was not added.");
                    error.Show();
                }
            }
        }
    }
}
