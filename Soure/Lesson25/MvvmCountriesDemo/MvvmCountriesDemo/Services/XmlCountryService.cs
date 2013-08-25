using MvvmCountriesDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvvmCountriesDemo.Services
{
    public class XmlCountryService
    {
        private static List<CountryData> countryList = null;

        static XmlCountryService()
        {
            XDocument loadedData = XDocument.Load("Datas/CountriesXML.xml");
            var data = from query in loadedData.Descendants("Country")
                       select new CountryData
                       {
                           Name = (string)query.Element("Name"),
                           Flag = (string)query.Element("Flag"),
                           Description = (string)query.Element("Description"),
                           Capital = (string)query.Element("Capital"),
                           ID = (int)query.Element("ID")
                       };
            countryList = data.ToList();
        }

        public System.Collections.Generic.IList<CountryData> GetCountryList()
        {
            return countryList;
        }

        public CountryData GetCountryById(int id)
        {
            return countryList.FirstOrDefault(c => c.ID == id);
        }
    }
}
