using FunctionClock.Models;
using FunctionClock.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace FunctionClock.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
        private DispatcherTimer _dispatcherTimer;
        public MainViewModel()
        {
            DisplayTime();
            StartDispatcherTimer();
            Setting = DataService.Current.GetSetting();
            Messenger.Default.Register<Setting>(this,"ClockSettingUpdated", setting =>
                {
                    Setting = setting;
                });
        }

        private void StartDispatcherTimer()
        {
            _dispatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _dispatcherTimer.Start();
        }

        void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            DisplayTime();
        }

        private void DisplayTime()
        {
            DateTime now = DateTime.Now;
            DayOfWeek = now.DayOfWeek;
            Time = now.ToString("HH:mm");
            Seconds = now.ToString("ss");
            TimeOfDay = now.ToString("tt");
        }

        private DayOfWeek _dayOfWeek;
        public DayOfWeek DayOfWeek
        {
            get
            {
                return _dayOfWeek;
            }
            set
            {
                if (_dayOfWeek == value)
                    return;
                _dayOfWeek = value;
                this.RaisePropertyChanged("DayOfWeek");
            }
        }

        private string _seconds;
        public string Seconds
        {
            get
            {
                return _seconds;
            }
            set
            {
                if (_seconds == value)
                    return;
                _seconds = value;
                this.RaisePropertyChanged("Seconds");
            }
        }

        private bool _showSeconds;
        public bool ShowSeconds
        {
            get
            {
                return _showSeconds;
            }
            set
            {
                if (_showSeconds == value)
                    return;
                _showSeconds = value;
                this.RaisePropertyChanged("ShowSeconds");
            }
        }

        private string _wakeTime;
        public string WakeTime
        {
            get
            {
                return _wakeTime;
            }
            set
            {
                if (_wakeTime == value)
                    return;
                _wakeTime = value;
                this.RaisePropertyChanged("WakeTime");
            }
        }

        private bool _showWakeTime;
        public bool ShowWakeTime
        {
            get
            {
                return _showWakeTime;
            }
            set
            {
                if (_showWakeTime == value)
                    return;
                _showWakeTime = value;
                this.RaisePropertyChanged("ShowWakeTime");
            }
        }

        private string _time;
        public string Time
        {
            get
            {
                return _time;
            }
            set
            {
                if (_time == value)
                    return;
                _time = value;
                this.RaisePropertyChanged("Time");
            }
        }

        private string _timeOfDay;
        public string TimeOfDay
        {
            get
            {
                return _timeOfDay;
            }
            set
            {
                if (_timeOfDay == value)
                    return;
                _timeOfDay = value;
                this.RaisePropertyChanged("TimeOfDay");
            }
        }

        private Setting _setting;
        public Setting Setting
        {
            get
            {
                return _setting;
            }
            set
            {
                _setting = value;
                this.RaisePropertyChanged("Setting");
            }
        }
    }
}
