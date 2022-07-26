using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipyModules.Models
{
    public class RecipyStepData : BindableBase
    {
        int step;
        public int Step
        {
            get { return step; }
            set { SetProperty(ref step, value); }
        }

        int cycle;
        public int Cycle
        {
            get { return cycle; }
            set { SetProperty(ref cycle, value); }
        }

        string type;
        public string Type
        {
            get { return type; }
            set { SetProperty(ref type, value); }
        }

        string mode;
        public string Mode
        {
            get { return mode; }
            set { SetProperty(ref mode, value); }
        }

        double volt;
        public double Volt
        {
            get { return volt; }
            set { SetProperty(ref volt, value); }
        }

        double curr;
        public double Curr
        {
            get { return curr; }
            set { SetProperty(ref curr, value); }
        }

        double power;
        public double Power
        {
            get { return power; }
            set { SetProperty(ref power, value); }
        }

        double reg;
        public double Reg
        {
            get { return reg; }
            set { SetProperty(ref reg, value); }
        }

        double temp;
        public double Temp
        {
            get { return temp; }
            set { SetProperty(ref temp, value); }
        }

        bool tempWait;
        public bool TempWait
        {
            get { return tempWait; }
            set { SetProperty(ref tempWait, value); }
        }

        string endCondi;
        public string EndCondi
        {
            get { return endCondi; }
            set { SetProperty(ref endCondi, value); }
        }

        string saftyCondi;
        public string SaftyCondi
        {
            get { return saftyCondi; }
            set { SetProperty(ref saftyCondi, value); }
        }

        string saveCondi;
        public string SaveCondi
        {
            get { return saveCondi; }
            set { SetProperty(ref saveCondi, value); }
        }

        string explan;
        public string Explan
        {
            get { return explan; }
            set { SetProperty(ref explan, value); }
        }
    }
}
