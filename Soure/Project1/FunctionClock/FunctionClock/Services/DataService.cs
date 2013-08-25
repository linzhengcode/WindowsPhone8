using FunctionClock.Models;
using Microsoft.Phone.Scheduler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionClock.Services
{
    public class DataService
    {
        private const string ALARMSKEY = "ALARMSKEY";
        private const string SETTINGKEY = "SETTINGKEY";
        private const string Monday = "_Monday";
        private const string Tuesday = "_Tuesday";
        private const string Wednesday = "_Wednesday";
        private const string Thursday = "_Thursday";
        private const string Friday = "_Friday";
        private const string Saturday = "_Saturday";
        private const string Sunday = "_Sunday";

        private static DataService _dataService;

        public static DataService Current
        {
            get
            {
                if (_dataService == null)
                {
                    _dataService = new DataService();
                }
                return _dataService;
            }
        }

        private DataService()
        {

        }

        public AlarmModel CreateNewAlarm()
        {
            DateTime now = DateTime.Now;
            return new AlarmModel
            {
                ID = Guid.NewGuid(),
                Enable = true,
                Name = "闹铃名字",
                RepeatDaysOfWeek = new RepeatDaysOfWeek(),
                Sound = Sounds.GetSounds().First(),
                WakeTime = new DateTime(now.Year, now.Month, now.Day, 10, 0, 0)
            };
        }

        public void DeleteAlarm(AlarmModel alarm)
        {
            if (alarm == null) return;
            var alarms = GetAllAlarms();
            AlarmModel alarmTemp = GetAlarmByID(alarm.ID);
            if (alarmTemp != null)
            {
                alarms.Remove(alarm);
                IsolatedStorageSettings.ApplicationSettings[ALARMSKEY] = alarms;
                IsolatedStorageSettings.ApplicationSettings.Save();
                ClearRegisterAlarm(alarm);
            }       
        }

        public ObservableCollection<AlarmModel> GetAllAlarms()
        {
            ObservableCollection<AlarmModel> alarms;
            if (IsolatedStorageSettings.ApplicationSettings.Contains(ALARMSKEY))
            {
                alarms = (ObservableCollection<AlarmModel>)IsolatedStorageSettings.ApplicationSettings[ALARMSKEY];
            }
            else
            {
                alarms = new ObservableCollection<AlarmModel>();
                IsolatedStorageSettings.ApplicationSettings[ALARMSKEY] = alarms;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
            return alarms;
        }

        public AlarmModel GetAlarmByID(Guid id)
        {
            return GetAllAlarms().SingleOrDefault<AlarmModel>(item => item.ID == id);
        }

        public void SaveAlarm(AlarmModel alarm)
        {
            if (alarm == null) return;
            var alarms = GetAllAlarms();
            AlarmModel alarmTemp = GetAlarmByID(alarm.ID);
            if (alarmTemp == null)
            {
                alarms.Add(alarm);
            }
            else
            {
                alarms[alarms.IndexOf(alarmTemp)] = alarm;
            }
            IsolatedStorageSettings.ApplicationSettings[ALARMSKEY] = alarms;
            IsolatedStorageSettings.ApplicationSettings.Save();

            if (alarm.Enable)
            {
                RegisterAlarm(alarm);
            }
            else
            {
                ClearRegisterAlarm(alarm);
            }
        }

        private void RegisterAlarm(AlarmModel alarmModel)
        {
            ClearRegisterAlarm(alarmModel);
            RepeatDaysOfWeek repeatDaysOfWeek = alarmModel.RepeatDaysOfWeek;
            if (repeatDaysOfWeek != null)
            {
                bool onlyOnce=true;
                if (repeatDaysOfWeek.Monday)
                {
                    onlyOnce=false;
                    DateTime beiginTime = GetAlarmBeiginDateTime(alarmModel.WakeTime, DayOfWeek.Monday);
                    RegisterAlarm(alarmModel, alarmModel.ID + Monday, beiginTime);
                }
                if (repeatDaysOfWeek.Tuesday)
                {
                     onlyOnce=false;
                    DateTime beiginTime = GetAlarmBeiginDateTime(alarmModel.WakeTime, DayOfWeek.Tuesday);
                    RegisterAlarm(alarmModel, alarmModel.ID + Tuesday, beiginTime);
                }
                if (repeatDaysOfWeek.Wednesday)
                {
                     onlyOnce=false;
                    DateTime beiginTime = GetAlarmBeiginDateTime(alarmModel.WakeTime, DayOfWeek.Wednesday);
                    RegisterAlarm(alarmModel, alarmModel.ID + Wednesday, beiginTime);
                }
                if (repeatDaysOfWeek.Thursday)
                {
                     onlyOnce=false;
                    DateTime beiginTime = GetAlarmBeiginDateTime(alarmModel.WakeTime, DayOfWeek.Thursday);
                    RegisterAlarm(alarmModel, alarmModel.ID + Thursday, beiginTime);
                }
                if (repeatDaysOfWeek.Friday)
                {
                     onlyOnce=false;
                    DateTime beiginTime = GetAlarmBeiginDateTime(alarmModel.WakeTime, DayOfWeek.Friday);
                    RegisterAlarm(alarmModel, alarmModel.ID + Friday, beiginTime);
                }
                if (repeatDaysOfWeek.Saturday)
                {
                     onlyOnce=false;
                    DateTime beiginTime = GetAlarmBeiginDateTime(alarmModel.WakeTime, DayOfWeek.Saturday);
                    RegisterAlarm(alarmModel, alarmModel.ID + Saturday, beiginTime);
                }
                if (repeatDaysOfWeek.Sunday)
                {
                     onlyOnce=false;
                    DateTime beiginTime = GetAlarmBeiginDateTime(alarmModel.WakeTime, DayOfWeek.Sunday);
                    RegisterAlarm(alarmModel, alarmModel.ID + Sunday, beiginTime);
                }
                if(onlyOnce)
                RegisterAlarm(alarmModel, alarmModel.ID.ToString(), alarmModel.WakeTime, RecurrenceInterval.None);
            }
            else
            {
                RegisterAlarm(alarmModel, alarmModel.ID.ToString(), alarmModel.WakeTime, RecurrenceInterval.None);
            }

        }

        private DateTime GetAlarmBeiginDateTime(DateTime wakeTime, DayOfWeek dayOfWeek)
        {
            DayOfWeek nowDayOfWeek = DateTime.Now.DayOfWeek;
            DateTime temp = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, wakeTime.Hour, wakeTime.Minute, wakeTime.Second);
            int day = Math.Abs(nowDayOfWeek - nowDayOfWeek);
            TimeSpan space = new TimeSpan(day, 0, 0, 0);
            DateTime beiginTime = temp.Subtract(space);
            return beiginTime;
        }



        private void RegisterAlarm(AlarmModel alarmModel, string alarmID, DateTime beginTime, RecurrenceInterval recurrenceInterval = RecurrenceInterval.Weekly)
        {
            bool haveFound;
            Alarm alarm = ScheduledActionService.Find(alarmID) as Alarm;
            if (alarm == null)
            {
                haveFound = false;
                alarm = new Alarm(alarmID);
            }
            else
            {
                haveFound = true;
            }
            alarm.Content = alarmModel.Name;
            alarm.Sound = alarmModel.Sound.Uri;
            alarm.RecurrenceType = recurrenceInterval;
            if (beginTime < DateTime.Now && recurrenceInterval== RecurrenceInterval.None)
            {
                alarm.BeginTime = beginTime.Add(new TimeSpan(1, 0, 0, 0));
            }
            else
            {
                alarm.BeginTime = beginTime;
            }
            if (haveFound)
            {
                ScheduledActionService.Replace(alarm);
            }
            else
            {
                ScheduledActionService.Add(alarm);
            }
        }

        private void ClearRegisterAlarm(string alarmID)
        {
            Alarm alarm = ScheduledActionService.Find(alarmID) as Alarm;
            if (alarm != null)
            {
                ScheduledActionService.Remove(alarmID);
            }
        }

        private void ClearRegisterAlarm(AlarmModel alarmModel)
        {
            ClearRegisterAlarm(alarmModel.ID.ToString());
            ClearRegisterAlarm(alarmModel.ID.ToString() + Monday);
            ClearRegisterAlarm(alarmModel.ID.ToString() + Tuesday);
            ClearRegisterAlarm(alarmModel.ID.ToString() + Wednesday);
            ClearRegisterAlarm(alarmModel.ID.ToString() + Thursday);
            ClearRegisterAlarm(alarmModel.ID.ToString() + Friday);
            ClearRegisterAlarm(alarmModel.ID.ToString() + Saturday);
            ClearRegisterAlarm(alarmModel.ID.ToString() + Sunday);
        }

        public void SaveSetting(Setting setting)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(SETTINGKEY))
            {
                IsolatedStorageSettings.ApplicationSettings[SETTINGKEY] = setting;
            }
            else
            {
                IsolatedStorageSettings.ApplicationSettings.Add(SETTINGKEY, setting);
            }
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        public Setting GetSetting()
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(SETTINGKEY))
            {
                return (Setting)IsolatedStorageSettings.ApplicationSettings[SETTINGKEY];
            }
            else
            {
                return new Setting();
            }
        }

    }
}
