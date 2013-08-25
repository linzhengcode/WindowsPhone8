using Microsoft.Expression.Interactivity.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MVVM_2_Demo
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public EventHandler UIStoryboard;

        private ICommand _manipulationStartedCommand;
        public ICommand ManipulationStartedCommand
        {
            get
            {
                return _manipulationStartedCommand;
            }
            set
            {
                if (_manipulationStartedCommand == value)
                    return;
                _manipulationStartedCommand = value;
                NotifyPropertyChanged("ManipulationStartedCommand");
            }
        }

        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                if (_message == value)
                    return;
                _message = value;
                NotifyPropertyChanged("Message");
            }
        }

        public MainViewModel()
        {
            ManipulationStartedCommand = new ActionCommand(param =>
            {
                Point point = (Point)param;
                if (point != null)
                {
                    Message = "x:" + point.X + "  Y:" + point.Y;
                }

                if (UIStoryboard != null)
                {
                    UIStoryboard.Invoke(this, EventArgs.Empty);
                }
                
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
