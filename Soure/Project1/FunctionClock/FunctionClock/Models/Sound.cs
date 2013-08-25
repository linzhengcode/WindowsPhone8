using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionClock.Models
{
    public class Sound : ModelBase
    {
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

        private Uri _uri;
        public Uri Uri
        {
            get
            {
                return _uri;
            }
            set
            {
                if (_uri == value)
                    return;
                _uri = value;
                OnPropertyChanged("Uri");
            }
        }
    }
}
