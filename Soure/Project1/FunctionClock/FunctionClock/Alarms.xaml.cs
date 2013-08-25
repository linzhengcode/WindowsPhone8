using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using FunctionClock.ViewModels;

namespace FunctionClock
{
    public partial class Alarms : PhoneApplicationPage
    {
        public Alarms()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();
        }

        // 用于生成本地化 ApplicationBar 的示例代码
        private void BuildLocalizedApplicationBar()
        {
            // 将页面的 ApplicationBar 设置为 ApplicationBar 的新实例。
            ApplicationBar = new ApplicationBar();

            ApplicationBarIconButton savebuton = new ApplicationBarIconButton();
            savebuton.Text = "添加";
            savebuton.Click += savebuton_Click;
            savebuton.IconUri = new Uri("/Images/appbar.add.rest.png", UriKind.Relative);
            ApplicationBar.Buttons.Add(savebuton);

        }

        void savebuton_Click(object sender, EventArgs e)
        {
            ((AlarmsViewModel)base.DataContext).AddAlarmCommand.Execute(null);
        }
    }
}