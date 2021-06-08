using Prism.Mvvm;
using System;
using System.IO;
using System.Timers;

namespace Title.ViewModels
{
    public class TitleBarViewModel : BindableBase
    {
        private readonly Timer PcTimer = new Timer();

        private DateTime _time = DateTime.Now;
        public DateTime Time
        {
            get { return _time; }
            set { SetProperty(ref _time, value); }
        }

        private long _maxVolume;
        public long MaxVolume
        {
            get { return _maxVolume; }
            set { SetProperty(ref _maxVolume, value); }
        }

        private long _useVolume;
        public long UseVolume
        {
            get { return _useVolume; }
            set { SetProperty(ref _useVolume, value); }
        }

        private double _usePersent;
        public double UsePersent
        {
            get { return _usePersent; }
            set { SetProperty(ref _usePersent, value); }
        }

        public TitleBarViewModel()
        {
            PcTimer.Interval = 1000;
            PcTimer.Elapsed += (sender, e) => Time = DateTime.Now;
            PcTimer.Start();

            DriveVolumeCalc("C", out _maxVolume, out _useVolume, out _usePersent);
        }

        public bool DriveVolumeCalc(string driveName, out long max, out long use, out double persent)
        {
            try
            {
                DriveInfo drive = new DriveInfo(driveName);

                max = drive.TotalSize;
                use = max - drive.AvailableFreeSpace;
                persent = Math.Round(use / (double)drive.AvailableFreeSpace * 100, 2);

                return true;
            }
            catch (Exception)
            {
                max = use = 0;
                persent = 0;
                return false;
            }
        }
    }
}
