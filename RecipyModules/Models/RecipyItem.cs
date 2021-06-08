using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipyModules.Models
{
    public class RecipyItem : BindableBase
    {
        string type;
        public string Type
        {
            get { return type; }
            set { SetProperty(ref type, value); }
        }

        string fullPath;
        public string FullPath
        {
            get { return fullPath; }
            set { SetProperty(ref fullPath, value); }
        }

        string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private ObservableCollection<RecipyItem> child = new ObservableCollection<RecipyItem>();
        public ObservableCollection<RecipyItem> Child
        {
            get { return child; }
            set { SetProperty(ref child, value); }
        }
    }
}
