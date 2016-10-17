using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesign
{
    public class DemoItem : INotifyPropertyChanged
    {
        private string _name;
        private object _content;
        private string _source;

        public string Name
        {
            get { return _name; }
            set
            {
                this.MutateVerbose(ref _name, value, RaisePropertyChanged());
            }
        }

        public object Content
        {
            get { return _content; }
            set
            {
                this.MutateVerbose(ref _content, value, RaisePropertyChanged());
            }
        }

        public string Source
        {
            get { return _source; }
            set
            {
                this.MutateVerbose(ref _source, value, RaisePropertyChanged());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
