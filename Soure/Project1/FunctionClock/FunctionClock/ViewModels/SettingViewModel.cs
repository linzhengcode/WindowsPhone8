using FunctionClock.Models;
using FunctionClock.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionClock.ViewModels
{
    public class SettingViewModel : ViewModelBase
    {
        public SettingViewModel()
        {
            Setting = DataService.Current.GetSetting();
            SaveCommand = new RelayCommand(() =>
                {
                    DataService.Current.SaveSetting(Setting);
                    Messenger.Default.Send<Setting>(Setting, "ClockSettingUpdated");
                });
        }

        public Setting Setting { get; set; }
        public RelayCommand SaveCommand { get; set; }
    }
}
