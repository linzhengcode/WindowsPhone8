using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionClock.Models
{
    public class AlarmModel : ModelBase
    {
        private bool _enable = true;
        public bool Enable
        {
            get
            {
                return _enable;
            }
            set
            {
                if (_enable == value)
                    return;
                _enable = value;
                OnPropertyChanged("Enable");
            }
        }

        private Guid _id;
        public Guid ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id == value)
                    return;
                _id = value;
                OnPropertyChanged("ID");
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name == value)
                    return;
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private RepeatDaysOfWeek _repeatDaysOfWeek;
        public RepeatDaysOfWeek RepeatDaysOfWeek
        {
            get
            {
                return _repeatDaysOfWeek;
            }
            set
            {
                //if (_repeatDaysOfWeek == value)
                //    return;
                _repeatDaysOfWeek = value;
                OnPropertyChanged("RepeatDaysOfWeek");
            }
        }

        private Sound _sound;
        public Sound Sound
        {
            get
            {
                return _sound;
            }
            set
            {
                //if (_sound == value)
                //    return;
                _sound = value;
                OnPropertyChanged("Sound");
            }
        }

        private DateTime _wakeTime;
        public DateTime WakeTime
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
                OnPropertyChanged("WakeTime");
            }
        }
    }
}
