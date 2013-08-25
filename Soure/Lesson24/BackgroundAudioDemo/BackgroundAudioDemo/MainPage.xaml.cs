using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BackgroundAudioDemo.Resources;
using System.IO.IsolatedStorage;
using System.Windows.Resources;
using Microsoft.Phone.BackgroundAudio;

namespace BackgroundAudioDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        BackgroundAudioPlayer backgroundAudioPlayer;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            backgroundAudioPlayer= BackgroundAudioPlayer.Instance;
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!storage.FileExists("Making love without nothing at all.mp3"))
                {
                    StreamResourceInfo resource = Application.GetResourceStream(new Uri("Assets/Making love without nothing at all.mp3", UriKind.Relative));
                    using (IsolatedStorageFileStream file = storage.CreateFile("Making love without nothing at all.mp3"))
                    {
                        int chunkSize = 4096;
                        byte[] bytes = new byte[chunkSize];
                        int byteCount;
                        while ((byteCount = resource.Stream.Read(bytes, 0, chunkSize)) > 0)
                        {
                            file.Write(bytes, 0, byteCount);
                        }
                    }
                }
            }

            backgroundAudioPlayer.PlayStateChanged += backgroundAudioPlayer_PlayStateChanged;


            base.OnNavigatedTo(e);
        }

        void backgroundAudioPlayer_PlayStateChanged(object sender, EventArgs e)
        {
            if (backgroundAudioPlayer.PlayerState == PlayState.Playing)
            {
                play.Content = "暂停播放";
            }
            else if (backgroundAudioPlayer.PlayerState == PlayState.Paused)
            {
                play.Content = "播放音乐";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (backgroundAudioPlayer.PlayerState == PlayState.Playing)
            {
                backgroundAudioPlayer.Pause();
            }
            else
            {
                backgroundAudioPlayer.Play();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            backgroundAudioPlayer.SkipNext();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            backgroundAudioPlayer.SkipPrevious();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (backgroundAudioPlayer.PlayerState == PlayState.Playing)
            {
                backgroundAudioPlayer.Rewind();
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (backgroundAudioPlayer.PlayerState == PlayState.Playing)
            {
                backgroundAudioPlayer.FastForward();
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