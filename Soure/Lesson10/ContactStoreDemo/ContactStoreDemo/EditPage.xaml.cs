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

namespace ContactStoreDemo
{
    public partial class EditPage : PhoneApplicationPage
    {
        string id;
        StoredContact storedContact;
        ContactStore contactStore;
        public EditPage()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.Keys.Contains("id"))
            {
                id = NavigationContext.QueryString["id"];
            }
            contactStore = await ContactStore.CreateOrOpenAsync(ContactStoreSystemAccessMode.ReadWrite, ContactStoreApplicationAccessMode.ReadOnly);
            storedContact = await contactStore.FindContactByIdAsync(id);
            if (storedContact != null)
            {
                var properties = await storedContact.GetPropertiesAsync();
                if (properties.Keys.Contains(KnownContactProperties.FamilyName))
                {
                    name.Text = properties[KnownContactProperties.FamilyName].ToString();
                }
                if (properties.Keys.Contains(KnownContactProperties.Telephone))
                {
                    tel.Text = properties[KnownContactProperties.Telephone].ToString();
                }
            }
            base.OnNavigatedTo(e);
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var properties = await storedContact.GetPropertiesAsync();
            if (properties.Keys.Contains(KnownContactProperties.FamilyName))
            {
                properties[KnownContactProperties.FamilyName] = name.Text;
            }
            else
            {
                properties.Add(KnownContactProperties.FamilyName, name.Text);
            }
            if (properties.Keys.Contains(KnownContactProperties.Telephone))
            {
                properties[KnownContactProperties.Telephone] = tel.Text;
            }
            else
            {
                properties.Add(KnownContactProperties.Telephone, tel.Text);
            }
            await storedContact.SaveAsync();
            NavigationService.GoBack();
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            await contactStore.DeleteContactAsync(id);
            NavigationService.GoBack();
        }
    }
}