using FunctionClock.Commons;
using FunctionClock.Models;
using FunctionClock.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionClock.ViewModels
{
    public class AlarmsViewModel : ViewModelBase
    {

        public AlarmsViewModel()
        {
            Alarms = DataService.Current.GetAllAlarms();
            AddAlarmCommand = new RelayCommand(() => NavigationHelper.NavigateTo(Uris.AddEditAlarmUri));
            EditAlarmCommand = new RelayCommand<AlarmModel>((alarm) => NavigationHelper.NavigateTo(Uris.AddEditAlarmUri, alarm));
           DeleteAlarmCommand=new RelayCommand<AlarmModel>(Alarm=>
           {
               DataService.Current.DeleteAlarm(Alarm);
           });

           EnableAlarmCommand = new RelayCommand<AlarmModel>(Alarm =>
             {
                 Alarm.Enable = !Alarm.Enable;
                 DataService.Current.SaveAlarm(Alarm);
             });
        
        }

        public ObservableCollection<AlarmModel> Alarms { get; set; }

        public RelayCommand AddAlarmCommand { get; set; }

        public RelayCommand<AlarmModel> EditAlarmCommand { get; set; }

        public RelayCommand<AlarmModel> DeleteAlarmCommand { get; set; }

        public RelayCommand<AlarmModel> EnableAlarmCommand { get; set; }
    }
}
