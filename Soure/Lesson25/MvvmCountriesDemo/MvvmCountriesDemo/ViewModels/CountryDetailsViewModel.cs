using MvvmCountriesDemo.Helpers;
using MvvmCountriesDemo.Models;
using MvvmCountriesDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmCountriesDemo.ViewModels
{
    public class CountryDetailsViewModel : ViewModelBase
    {
        public CountryDetailsViewModel()
        {
            XmlCountryService countryRepository = new XmlCountryService();
            CountryData country = countryRepository.GetCountryById(Navigation.Id);

            DataContext = country;
        }

        private CountryData _dataContext;

        public CountryData DataContext
        {
            get
            {
                return _dataContext;
            }
            set
            {
                if (_dataContext != value)
                {
                    _dataContext = value;
                }

                OnPropertyChanged("DataContext");
            }
        }
    }
}
