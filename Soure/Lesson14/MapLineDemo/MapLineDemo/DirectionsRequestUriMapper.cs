using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace MapLineDemo
{
    class DirectionsRequestUriMapper : UriMapperBase
    {
        public override Uri MapUri(Uri uri)
        {
            string tempUri = Uri.UnescapeDataString(uri.ToString());
            if (tempUri.Contains("ms-drive-to") || tempUri.Contains("ms-walk-to"))
            {
                char[] uriDelimiters = { '?', '=', '&' };
                string[] uriParameters = tempUri.Split(uriDelimiters);
                string destLatitude = uriParameters[4];
                string destLongitude = uriParameters[6];
                string destName = uriParameters[8];
                return new Uri(
                    "/MainPage.xaml?" +
                    "latitude=" + destLatitude + "&" +
                    "longitude=" + destLongitude + "&" +
                    "name=" + destName,
                    UriKind.Relative);
            }
            return uri;
        }
    }
}
