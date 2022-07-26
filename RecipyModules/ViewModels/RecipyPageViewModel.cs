using Prism.Commands;
using Prism.Mvvm;
using RecipyModules.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableModule;

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


        public ObservableCollection<RecipyStepData> recipyStepes = new ObservableCollection<RecipyStepData>();
        public ObservableCollection<RecipyStepData> RecipyStepes
        {
            get { return recipyStepes; }
            set { SetProperty(ref recipyStepes, value); }
        }

        private DataTable recipyTable = new DataTable();
        public DataTable RecipyTable
        {
            get { return recipyTable; }
            set {  SetProperty(ref recipyTable, value); }
        }

        public DelegateCommand<object> SelectedTreeItems { get; set; }

        public DelegateCommand RecipyStepAdd { get; set; }

        public RecipyPageViewModel()
        {
            RecipyTable = TableManager.ColumnAdd(RecipyTable,
                new string[] { "Step", "Cycle", "Type", "Mode", "Volt", "Curr", "Power", "Reg", "Temp", "TempWait", "EndCondi", "SaftyCondi", "SaveCondi", "Explan" },
                new Type[] { typeof(int), typeof(int), typeof(object), typeof(string), typeof(double), typeof(double), typeof(double), typeof(double), typeof(double), typeof(bool), typeof(string), typeof(string), typeof(string), typeof(string) });

            object[] bbb = new object[] { 1, 1, "CYCLE", "CC", 5, 5, 5, 5, 5, true, "sss", "eee", "qqq", "bbb" };
            RecipyTable = TableManager.RowAdd(RecipyTable, 0, bbb);

            RecipyStepes.Add(new RecipyStepData { Step=1, Cycle=1, Type="CYCLE", Mode="CC", Volt=5, Curr=6, Power=5, Reg=5, Temp=5, 
                 TempWait=true, EndCondi="sss", SaftyCondi = "eee", SaveCondi = "qqq", Explan="bbb" }); ;

            SelectedTreeItems = new DelegateCommand<object>(ChangeSelection);

            RecipyStepAdd = new DelegateCommand(StepAdd);

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

        private void StepAdd()
        {
            object[] bbb = new object[] { 1, 1, "CYCLE", "CC", 5, 5, 5, 5, 5, true, "sss", "eee", "qqq", "bbb" };
            RecipyTable = TableManager.RowAdd(RecipyTable, 1, bbb);

            //RecipyStepes.Add(new RecipyStepData
            //{
            //    Step = 1,
            //    Cycle = 1,
            //    Type = "CYCLE",
            //    Mode = "CC",
            //    Volt = 5,
            //    Curr = 6,
            //    Power = 7,
            //    Reg = 5,
            //    Temp = 5,
            //    TempWait = true,
            //    EndCondi = "sss",
            //    SaftyCondi = "eee",
            //    SaveCondi = "qqq",
            //    Explan = "bbb"
            //});
        }
    }
}