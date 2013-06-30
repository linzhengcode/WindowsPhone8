using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WalletDemo.Resources;
using Windows.System;
using Microsoft.Phone.Wallet;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Tasks;

namespace WalletDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            WalletItemCollection wc = await Wallet.GetItemsAsync();
            foreach (var item in wc)
            {
                if (item.Id == "deal1")
                {
                    dealbutton.IsEnabled = false;
                }

                if (item.Id == "walletTransactionItem1")
                {
                    transationbutton.IsEnabled = false;
                }

                if (item.Id == "paymentInstrument1")
                {
                    bank.IsEnabled = false;
                }
            }

            base.OnNavigatedTo(e);
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
           await  Launcher.LaunchUriAsync(new Uri("wallet://", UriKind.RelativeOrAbsolute));
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Deal deal = new Deal("deal1");
            deal.MerchantName = "某某公司";
            deal.DisplayName = "某某优惠卡";
            deal.IsUsed = false;
            await deal.SaveAsync();
            MessageBox.Show("添加优惠券信息成功");
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string msg = "";
            WalletItemCollection wc = await Wallet.GetItemsAsync();
            foreach (var item in wc)
            {
                msg += item.DisplayName+"|";
            }

            MessageBox.Show(msg);
        }

        private void transationbutton_Click(object sender, RoutedEventArgs e)
        {
            WalletTransactionItem walletTransactionItem = new WalletTransactionItem("walletTransactionItem1");
            walletTransactionItem.DisplayName = "会员卡";

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.SetSource(Application.GetResourceStream(new Uri("Assets/ApplicationIcon.png", UriKind.Relative)).Stream);
            walletTransactionItem.Logo159x159 = bitmapImage;
            walletTransactionItem.Logo336x336 = bitmapImage;
            walletTransactionItem.Logo99x99 = bitmapImage;

            AddWalletItemTask addWalletItemTask = new AddWalletItemTask();
            addWalletItemTask.Item = walletTransactionItem;
            addWalletItemTask.Completed += addWalletItemTask_Completed;
            addWalletItemTask.Show();
        }

        void addWalletItemTask_Completed(object sender, AddWalletItemResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                MessageBox.Show("添加成功");
            }
        }

        private void bank_Click(object sender, RoutedEventArgs e)
        {
            PaymentInstrument paymentInstrument = new PaymentInstrument("paymentInstrument1");
            paymentInstrument.DisplayName = "我的银行卡";
            paymentInstrument.PaymentInstrumentKinds = PaymentInstrumentKinds.Credit;
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.SetSource(Application.GetResourceStream(new Uri("Assets/ApplicationIcon.png", UriKind.Relative)).Stream);
            paymentInstrument.Logo159x159 = bitmapImage;
            paymentInstrument.Logo336x336 = bitmapImage;
            paymentInstrument.Logo99x99 = bitmapImage;

            AddWalletItemTask addWalletItemTask = new AddWalletItemTask();
            addWalletItemTask.Item = paymentInstrument;
            addWalletItemTask.Completed += addWalletItemTask_Completed;
            addWalletItemTask.Show();
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