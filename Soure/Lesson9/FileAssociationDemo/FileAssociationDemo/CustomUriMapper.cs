using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace FileAssociationDemo
{
    class CustomUriMapper: UriMapperBase
    {
        public override Uri MapUri(Uri uri)
        {
            ///FileTypeAssociation?fileToken=89819279-4fe0-4531-9f57-d633f0949a19
            string tempuri = uri.ToString();

            if(tempuri.Contains("/FileTypeAssociation"))
            {
                int fileIdIndex = tempuri.IndexOf("fileToken=") + 10;
                string fileID = tempuri.Substring(fileIdIndex);
                return new Uri("/LogPage.xaml?fileToken=" + fileID, UriKind.Relative);
            }
            return uri;
        }
    }
}
