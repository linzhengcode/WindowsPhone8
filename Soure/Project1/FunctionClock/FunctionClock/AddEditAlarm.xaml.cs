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
    public partial class AddEditAlarm : PhoneApplicationPage
    {
        public AddEditAlarm()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();
        }

        private void BuildLocalizedApplicationBar()
        {
            // 将页面的 ApplicationBar 设置为 ApplicationBar 的新实例。
            ApplicationBar = new ApplicationBar();

            ApplicationBarIconButton savebuton = new ApplicationBarIconButton();
            savebuton.Text = "保存";
            savebuton.Click += savebuton_Click;
            savebuton.IconUri = new Uri("/Images/appbar.save.rest.png", UriKind.Relative);
            ApplicationBar.Buttons.Add(savebuton);

        }

        void savebuton_Click(object sender, EventArgs e)
        {
            ((AddEditAlarmViewModel)base.DataContext).SaveCommand.Execute(null);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ((AddEditAlarmViewModel)base.DataContext).LoadDataCommand.Execute(null);
            base.OnNavigatedTo(e);
        }
    }
}