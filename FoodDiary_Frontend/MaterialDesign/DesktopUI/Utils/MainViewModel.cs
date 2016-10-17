using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesign.DesktopUI.Utils
{
    class MainViewModel
    {

        public ObservableCollection<TestClass> Errors { get; private set; }

        public MainViewModel()
        {
            Errors = new ObservableCollection<TestClass>();
            Errors.Add(new TestClass() { Date = "Globalization", Number = 75 });
            Errors.Add(new TestClass() { Date = "Features", Number = 2 });
            Errors.Add(new TestClass() { Date = "ContentTypes", Number = 12 });
            Errors.Add(new TestClass() { Date = "Correctness", Number = 83 });
            Errors.Add(new TestClass() { Date = "Best Practices", Number = 29 });
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

    // class which represent a data point in the chart
    public class TestClass
    {
        public string Date { get; set; }

        public int Number { get; set; }
    }
}
