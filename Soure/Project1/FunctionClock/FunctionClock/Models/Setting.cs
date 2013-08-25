using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FunctionClock.Models
{
    public class Setting: ModelBase
    {

        public Setting()
        {
            ShowSeconds = true;
            WeatherEnable = true;
            DispalyColor = Colors.Red;
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
                OnPropertyChanged("ShowSeconds");
            }
        }

        private bool _weatherEnable;
        public bool WeatherEnable
        {
            get
            {
                return _weatherEnable;
            }
            set
            {
                if (_weatherEnable == value)
                    return;
                _weatherEnable = value;
                OnPropertyChanged("WeatherEnable");
            }
        }

        private Color _dispalyColor;
        public Color DispalyColor
        {
            get
            {
                return _dispalyColor;
            }
            set
            {
                if (_dispalyColor == value)
                    return;
                _dispalyColor = value;
                OnPropertyChanged("DispalyColor");
            }
        }
    }
}
