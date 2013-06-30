using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SpeechDemo.Resources;
using Windows.Phone.Speech.Synthesis;
using Windows.Phone.Speech.Recognition;

namespace SpeechDemo
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

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
            await speechSynthesizer.SpeakTextAsync("你好");
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SpeechRecognizer speechRecognizer = new SpeechRecognizer();
            speechRecognizer.Grammars.AddGrammarFromList("color", new List<string>
            {
                "红色",
                "白色",
                "蓝色",
                "绿色"
            });
            try
            {
                var result = await speechRecognizer.RecognizeAsync();
                if (result.TextConfidence == SpeechRecognitionConfidence.Rejected)
                {
                    MessageBox.Show("语音识别不到");
                }
                else
                {
                    MessageBox.Show(result.Text);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("请检查是否接收语音隐私协议" + err.Message + err.HResult);
            }

                
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {


            SpeechRecognizerUI speechRecognizerUI = new SpeechRecognizerUI();
            speechRecognizerUI.Recognizer.Grammars.AddGrammarFromList("Number", new List<string>
            {
                "一",
                "二",
                "三",
                "四"
            });
            try
            {
                var result = await speechRecognizerUI.RecognizeWithUIAsync();
                if (result.ResultStatus == SpeechRecognitionUIStatus.Succeeded)
                {
                    MessageBox.Show(result.RecognitionResult.Text);
                }
                else
                {
                    MessageBox.Show("语音识别不到");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message + err.HResult);
            }
        }

        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            SpeechRecognizer speechRecognizer = new SpeechRecognizer();
            speechRecognizer.Grammars.AddGrammarFromUri("music", new Uri("ms-appx:///SRGSGrammar1.xml"));
            try
            {
                var result = await speechRecognizer.RecognizeAsync();
                if (result.TextConfidence == SpeechRecognitionConfidence.Rejected)
                {
                    MessageBox.Show("语音识别不到");
                }
                else
                {
                    string music = "";
                    if (result.Semantics.Keys.Contains("music"))
                    {
                        music = result.Semantics["music"].Value.ToString();
                    }
                    MessageBox.Show(result.Text + "|" + music);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("请检查是否接收语音隐私协议" + err.Message + err.HResult);
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