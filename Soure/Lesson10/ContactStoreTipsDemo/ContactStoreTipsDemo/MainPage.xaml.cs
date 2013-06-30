using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ContactStoreTipsDemo.Resources;
using Windows.Phone.PersonalInformation;
using System.IO;
using System.Diagnostics;
using Microsoft.Phone.UserData;

namespace ContactStoreTipsDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        ContactStore store;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            store = await ContactStore.CreateOrOpenAsync(ContactStoreSystemAccessMode.ReadWrite, ContactStoreApplicationAccessMode.ReadOnly);
            RemoteIdHelper remoteIdHelper = new RemoteIdHelper();
            await remoteIdHelper.SetRemoteIdGuid(store);
            base.OnNavigatedTo(e);
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ContactQueryResult result = store.CreateContactQuery();
            var contacts = await result.GetContactsAsync();
            foreach (var contact in contacts)
            {
                var stream = await contact.ToVcardAsync(VCardFormat.Version2_1);
                byte[] datas = StreamToBytes(stream.AsStreamForRead());
                string vcard = System.Text.Encoding.UTF8.GetString(datas, 0, datas.Length);
                Debug.WriteLine(vcard);
            }
        }

        public byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            return bytes;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Contacts contacts = new Contacts();
            contacts.SearchCompleted += contacts_SearchCompleted;
            contacts.SearchAsync("", FilterKind.None, null);
        }

        void contacts_SearchCompleted(object sender, ContactsSearchEventArgs e)
        {
            IEnumerable<Contact> contacts = e.Results;

            var contacts2 = from contact in contacts
                            where contact.Accounts.Where(temp => temp.Name == "ContactStoreTipsDemo").Count() == 0
                                  &&contact.PhoneNumbers.Count()!=0
                            select contact;

            foreach (var contact in contacts2)
            {
                Debug.WriteLine(contact.DisplayName);
            }
        }

        // 用于生成本地化 ApplicationBar 的示例代码
        //private void BuildLocalizedApplicationBar()
        //{
        //    // 将页面的 ApplicationBar 设置为 ApplicationBar 的新实例。
        //    ApplicationBar = new ApplicationBar();

        //    // 创建新按钮并将文本值设置为 AppResources 中的本地化字符串。
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // 使用 AppResources 中的本地化字符串创建新菜单项。
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}