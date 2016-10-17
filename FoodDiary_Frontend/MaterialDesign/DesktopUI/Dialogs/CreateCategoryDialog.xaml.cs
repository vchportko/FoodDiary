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
        public CreateCategoryDialog()
        {
            InitializeComponent();
        }

        
        private void CreateCategoryDB(object sender, RoutedEventArgs e)
        {
            // procedure insert to db
            var category = tbCategory.Text;
            Console.WriteLine(category);
        }
    }
}
