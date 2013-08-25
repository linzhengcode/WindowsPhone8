using FunctionClock.Commons;
using FunctionClock.Models;
using FunctionClock.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FunctionClock.ViewModels
{
    public class AddEditAlarmViewModel: ViewModelBase
    {

        public AddEditAlarmViewModel()
        {
            LoadDataCommand = new RelayCommand(() => LoadData());
            SaveCommand = new RelayCommand(() =>
                {
                    if (Alarm.Name == "")
                    {
                        MessageBox.Show("请输入闹铃名字");
                        return;
                    }
                    DataService.Current.SaveAlarm(Alarm);
                    NavigationHelper.GoBack();
                });
            SelectSoundCommand = new RelayCommand(() => NavigationHelper.NavigateTo(Uris.AlarmSoundsUri, Alarm));
            SelectRepeatDaysCommand = new RelayCommand(() => NavigationHelper.NavigateTo(Uris.AlarmRepeatDaysUri, Alarm));
        }

        public RelayCommand LoadDataCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand SelectSoundCommand { get; set; }
        public RelayCommand SelectRepeatDaysCommand { get; set; }

        public void LoadData()
        {
            AlarmModel _alarm = NavigationHelper.Parameter as AlarmModel;

            if (_alarm == null)
            {
                Title = "添加闹铃";
                Alarm = DataService.Current.CreateNewAlarm();
            }
            else
            {
                Title = "编辑闹铃";
                Alarm = _alarm;
            }
        }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (_title == value)
                    return;
                _title = value;
                this.RaisePropertyChanged("Title");
            }
        }
        private AlarmModel _alarm;
        public AlarmModel Alarm
        {
            get
            {
                return _alarm;
            }
            set
            {
                //if (_alarm == value)
                //    return;
                _alarm = value;
                this.RaisePropertyChanged("Alarm");
            }
        }
    }
}
