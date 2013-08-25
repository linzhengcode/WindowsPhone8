using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionClock.Commons
{
    public class Uris
    {
        public static readonly Uri AlarmsUri = new Uri("/Alarms.xaml", UriKind.Relative);
        public static readonly Uri SettingsUri = new Uri("/Settings.xaml", UriKind.Relative);
        public static readonly Uri AddEditAlarmUri = new Uri("/AddEditAlarm.xaml", UriKind.Relative);
        public static readonly Uri AlarmRepeatDaysUri = new Uri("/AlarmRepeatDays.xaml", UriKind.Relative);
        public static readonly Uri AlarmSoundsUri = new Uri("/AlarmSounds.xaml", UriKind.Relative);
    }
}
