using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ScrawlNote.Commons;

namespace ScrawlNote
{
    public partial class Setting : PhoneApplicationPage
    {
        public Setting()
        {
            InitializeComponent();

            string language = AppSettingHelper.GetValueOrDefault(AppSettingHelper.LanguageKey, "zh-CN");
            if (language == "zh-TW")
            {
                zh_TW.IsChecked = true;
            }
            else
            {
                zh_cn.IsChecked = true;
            }
        }

        private void Checked_1(object sender, RoutedEventArgs e)
        {
            if (zh_cn.IsChecked==true)
            {
                AppSettingHelper.AddOrUpdateValue(AppSettingHelper.LanguageKey, "zh-CN");
            }
            else
            {
                AppSettingHelper.AddOrUpdateValue(AppSettingHelper.LanguageKey, "zh-TW");
            }
        }
    }
}