using FunctionClock.Commons;
using FunctionClock.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionClock.ViewModels
{
    public class AlarmRepeatDaysViewModel : ViewModelBase
    {
        private AlarmModel _alarm;

        public AlarmRepeatDaysViewModel()
        {
            LoadDataCommand = new RelayCommand(() => LoadData());
            SaveCommand = new RelayCommand(() =>
                {
                    _alarm.RepeatDaysOfWeek = RepeatDaysOfWeek;
                    NavigationHelper.GoBack(_alarm);
                });
        }

        public void LoadData()
        {
            _alarm = NavigationHelper.Parameter as AlarmModel;
            if (_alarm != null)
            {
                RepeatDaysOfWeek = _alarm.RepeatDaysOfWeek;
            }
        }

        public RelayCommand LoadDataCommand { get; set; }

        public RelayCommand SaveCommand { get; set; }

        private RepeatDaysOfWeek _repeatDaysOfWeek;
        public RepeatDaysOfWeek RepeatDaysOfWeek
        {
            get
            {
                return _repeatDaysOfWeek;
            }
            set
            {
                if (_repeatDaysOfWeek == value)
                    return;
                _repeatDaysOfWeek = value;
                RaisePropertyChanged("RepeatDaysOfWeek");
            }
        }

    }
}
