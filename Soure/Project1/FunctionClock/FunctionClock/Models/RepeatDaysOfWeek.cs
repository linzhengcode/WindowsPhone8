using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionClock.Models
{
    public class RepeatDaysOfWeek: ModelBase
    {
        private bool _sunday = false;
        public bool Sunday
        {
            get
            {
                return _sunday;
            }
            set
            {
                if (_sunday == value)
                    return;
                _sunday = value;
                OnPropertyChanged("Sunday");
            }
        }

        private bool _monday = false;
        public bool Monday
        {
            get
            {
                return _monday;
            }
            set
            {
                if (_monday == value)
                    return;
                _monday = value;
                OnPropertyChanged("Monday");
            }
        }

        private bool _tuesday = false;
        public bool Tuesday
        {
            get
            {
                return _tuesday;
            }
            set
            {
                if (_tuesday == value)
                    return;
                _tuesday = value;
                OnPropertyChanged("Tuesday");
            }
        }

        private bool _wednesday = false;
        public bool Wednesday
        {
            get
            {
                return _wednesday;
            }
            set
            {
                if (_wednesday == value)
                    return;
                _wednesday = value;
                OnPropertyChanged("Wednesday");
            }
        }

        private bool _thursday = false;
        public bool Thursday
        {
            get
            {
                return _thursday;
            }
            set
            {
                if (_thursday == value)
                    return;
                _thursday = value;
                OnPropertyChanged("Thursday");
            }
        }

        private bool _friday = false;
        public bool Friday
        {
            get
            {
                return _friday;
            }
            set
            {
                if (_friday == value)
                    return;
                _friday = value;
                OnPropertyChanged("Friday");
            }
        }

        private bool _saturday = false;
        public bool Saturday
        {
            get
            {
                return _saturday;
            }
            set
            {
                if (_saturday == value)
                    return;
                _saturday = value;
                OnPropertyChanged("Saturday");
            }
        }

    }
}
