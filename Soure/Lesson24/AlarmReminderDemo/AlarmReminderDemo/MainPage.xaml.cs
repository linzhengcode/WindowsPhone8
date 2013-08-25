using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using AlarmReminderDemo.Resources;
using Microsoft.Phone.Scheduler;

namespace AlarmReminderDemo
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var alarms = ScheduledActionService.GetActions<Alarm>();
            lls.ItemsSource = alarms.ToList();

            var reminders = ScheduledActionService.GetActions<Reminder>();
            lls2.ItemsSource = reminders.ToList();
            base.OnNavigatedTo(e);
        }

        int i = 0;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            i++;
            bool isAlarmExist;
            Alarm alarm = ScheduledActionService.Find("Alarm" + i) as Alarm;
            if (alarm == null)
            {
                isAlarmExist = false;
                alarm = new Alarm("Alarm" + i);
            }
            else
            {
                isAlarmExist = true;
            }

            alarm.BeginTime = DateTime.Now.AddMinutes(1);
            alarm.ExpirationTime = DateTime.Now.AddMinutes(2);
            alarm.Content = "闹钟" + i;
            alarm.Sound = new Uri("/Assets/Making love without nothing at all.mp3", UriKind.Relative);
            alarm.RecurrenceType = RecurrenceInterval.Daily;

            if (!isAlarmExist)
                ScheduledActionService.Add(alarm);
            else
                ScheduledActionService.Replace(alarm);

            var alarms = ScheduledActionService.GetActions<Alarm>();
            lls.ItemsSource = alarms.ToList();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            i++;
            bool isReminderExist;
            Reminder reminder = ScheduledActionService.Find("Reminder" + i) as Reminder;
            if (reminder == null)
            {
                isReminderExist = false;
                reminder = new Reminder("Reminder" + i);
            }
            else
            {
                isReminderExist = true;
            }

            reminder.BeginTime = DateTime.Now.AddMinutes(1);
            reminder.ExpirationTime = DateTime.Now.AddMinutes(2);
            reminder.Content = "提醒" + i;
            reminder.NavigationUri = new Uri("/Page1.xaml", UriKind.Relative);
            reminder.RecurrenceType = RecurrenceInterval.Daily;

            if (!isReminderExist)
                ScheduledActionService.Add(reminder);
            else
                ScheduledActionService.Replace(reminder);

            var reminders = ScheduledActionService.GetActions<Reminder>();
            lls2.ItemsSource = reminders.ToList();
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