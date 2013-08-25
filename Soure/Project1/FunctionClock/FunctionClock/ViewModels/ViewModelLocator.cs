using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Ioc;

namespace FunctionClock.ViewModels
{
    public class ViewModelLocator
    {
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public AlarmSoundsViewModel AlarmSounds
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AlarmSoundsViewModel>();
            }
        }

        public AlarmRepeatDaysViewModel AlarmRepeatDays
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AlarmRepeatDaysViewModel>();
            }
        }

        public AlarmsViewModel Alarms
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AlarmsViewModel>();
            }

        }

        public AddEditAlarmViewModel AddEditAlarm
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddEditAlarmViewModel>();
            }
        }

        public WeatherViewModel Weather
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WeatherViewModel>();
            }
        }

        public SettingViewModel Setting
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingViewModel>();
            }
        }

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<AlarmSoundsViewModel>();
            SimpleIoc.Default.Register<AlarmRepeatDaysViewModel>();
            SimpleIoc.Default.Register<AlarmsViewModel>();
            SimpleIoc.Default.Register<AddEditAlarmViewModel>();
            SimpleIoc.Default.Register<WeatherViewModel>();
            SimpleIoc.Default.Register<SettingViewModel>();
        }
    }
}
