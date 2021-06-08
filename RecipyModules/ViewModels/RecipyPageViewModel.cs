using Prism.Commands;
using Prism.Mvvm;
using RecipyModules.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipyModules.ViewModels
{
    public class RecipyPageViewModel : BindableBase
    {
        public ObservableCollection<RecipyItem> recipyTreeItem = new ObservableCollection<RecipyItem>();
        public ObservableCollection<RecipyItem> RecipyTreeItem
        {
            get { return recipyTreeItem; }
            set { SetProperty(ref recipyTreeItem, value); }
        }

        public DelegateCommand<object> SelectedTreeItems { get; set; }

        public RecipyPageViewModel()
        {
            //SelectedTreeItems = new DelegateCommand<object>(ChangeSelection);

            string path = "./Process/";

            DirectoryInfo info = new DirectoryInfo(path);
            RecipyTreeItem = new ObservableCollection<RecipyItem>();

            RecipyItem recipyItem = new RecipyItem
            {
                Type = "Folder",
                FullPath = info.FullName,
                Name = info.Name,
                Child = new ObservableCollection<RecipyItem>()
                
            };

            recipyTreeItem.Add(recipyItem);

            TreeFinder(path, recipyItem);

        }

        private void TreeFinder(string path, RecipyItem item)
        {
            foreach (var folder in Directory.GetDirectories(path))
            {
                DirectoryInfo info = new DirectoryInfo(folder);

                RecipyItem recipyItem = new RecipyItem
                {
                    Type = "Folder",
                    FullPath = info.FullName,
                    Name = info.Name,
                    Child = new ObservableCollection<RecipyItem>()
                };

                item.Child.Add(recipyItem);
                TreeFinder(folder, recipyItem);
            }

            foreach (var file in Directory.GetFiles(path))
            {
                FileInfo info = new FileInfo(file);

                RecipyItem recipyItem = new RecipyItem
                {
                    Type = "File",
                    FullPath = info.FullName,
                    Name = info.Name
                };

                item.Child.Add(recipyItem);
            }
        }

        private void ChangeSelection(object SeletedItem)
        {

        }
    }
}