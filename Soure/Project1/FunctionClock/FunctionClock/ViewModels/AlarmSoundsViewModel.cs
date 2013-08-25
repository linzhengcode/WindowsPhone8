using FunctionClock.Commons;
using FunctionClock.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionClock.ViewModels
{
    public class AlarmSoundsViewModel: ViewModelBase
    {
        private AlarmModel _alarm;

        public AlarmSoundsViewModel()
        {
            SoundList = Sounds.GetSounds();
            LoadDataCommand = new RelayCommand(() => LoadData());
            SelectCommand = new RelayCommand<Sound>(sound =>
                {
                    _alarm.Sound = sound;
                    NavigationHelper.GoBack(_alarm);
                });
            
        }

        public void LoadData()
        {
            _alarm = NavigationHelper.Parameter as AlarmModel;
        }

        public RelayCommand LoadDataCommand { get; set; }

        public RelayCommand<Sound> SelectCommand { get; set; }

        public List<Sound> SoundList { get; set; }
    }
}
