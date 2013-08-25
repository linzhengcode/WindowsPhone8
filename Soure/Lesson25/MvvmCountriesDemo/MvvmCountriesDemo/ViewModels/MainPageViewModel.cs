using MvvmCountriesDemo.Commands;
using MvvmCountriesDemo.Helpers;
using MvvmCountriesDemo.Models;
using MvvmCountriesDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace MvvmCountriesDemo.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {
            XmlCountryService countryRepository = new XmlCountryService();
            ListItemsSource = countryRepository.GetCountryList();
        }

        private IList<CountryData> _listItemsSource;

        public IList<CountryData> ListItemsSource
        {
            get
            {
                return _listItemsSource;
            }
            set
            {
                if (_listItemsSource != value)
                {
                    _listItemsSource = value;
                }

                OnPropertyChanged("ListItemsSource");
            }
        }

        #region Commands

        public ICommand SetCountryIdCommand
        {
            get
            {
                return new DelegateCommand(SetCountryId, CanSetCountryId);
            }
        }

        private void SetCountryId(object parameter)
        {
            CountryData selectedItemData = parameter as CountryData;

            if (selectedItemData != null)
            {
                Navigation.Id = selectedItemData.ID;
                Navigation.GotoCountryDetails();    
            }
        }

        private bool CanSetCountryId(object parameter)
        {


            return true;
        }

        #endregion

    }
}
