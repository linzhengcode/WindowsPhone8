using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionClock.Models
{
    public class Weather : ModelBase
    {
        private string _area;
        public string Area
        {
            get
            {
                return _area;
            }
            set
            {
                if (_area == value)
                    return;
                _area = value;
                OnPropertyChanged("Area");
            }
        }

        private int _temp;
        public int Temp
        {
            get
            {
                return _temp;
            }
            set
            {
                if (_temp == value)
                    return;
                _temp = value;
                OnPropertyChanged("Temp");
            }
        }

        private DegreeType _degreeType;
        public DegreeType DegreeType
        {
            get
            {
                return _degreeType;
            }
            set
            {
                _degreeType = value;
                OnPropertyChanged("DegreeType");
            }
        }
    }
}
