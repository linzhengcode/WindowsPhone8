using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Phone.PersonalInformation;
using Microsoft.Phone.Tasks;
using System.IO;
using Windows.Storage.Streams;

namespace ContactStoreDemo
{
    public partial class AddPage : PhoneApplicationPage
    {
        public AddPage()
        {
            InitializeComponent();
        }
        IInputStream stream;
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ContactStore contactStore = await ContactStore.CreateOrOpenAsync(ContactStoreSystemAccessMode.ReadWrite, ContactStoreApplicationAccessMode.ReadOnly);
            ContactInformation contactInformation=new ContactInformation();
            var properties = await contactInformation.GetPropertiesAsync();
            properties.Add(KnownContactProperties.FamilyName, name.Text);
            properties.Add(KnownContactProperties.Telephone, tel.Text);
            
            StoredContact storedContact = new StoredContact(contactStore, contactInformation);
            if (stream != null)
            {
                await storedContact.SetDisplayPictureAsync(stream);
            }
            await storedContact.SaveAsync();
            NavigationService.GoBack();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var photoChooser = new PhotoChooserTask();
            photoChooser.PixelHeight = 170;
            photoChooser.PixelWidth = 170;
            photoChooser.Completed += photoChooser_Completed;
            photoChooser.Show();
        }

        void photoChooser_Completed(object sender, PhotoResult e)
        {
            if (e.ChosenPhoto == null)
            {
                return;
            }
           stream=  e.ChosenPhoto.AsInputStream();
        }
    }
}