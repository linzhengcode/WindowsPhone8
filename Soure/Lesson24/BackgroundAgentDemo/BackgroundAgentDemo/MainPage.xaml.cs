using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BackgroundAgentDemo.Resources;
using Microsoft.Phone.Scheduler;

namespace BackgroundAgentDemo
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string name="PeriodicTask";
            PeriodicTask periodicTask = ScheduledActionService.Find(name) as PeriodicTask;
            if (periodicTask == null)
            {
                periodicTask = new PeriodicTask(name);
            }
            else
            {
                ScheduledActionService.Remove(name);
                periodicTask = new PeriodicTask(name);
            }
            periodicTask.Description = "描述我们的PeriodicTask后台任务是干什么的"; 
            try
            {
                ScheduledActionService.Add(periodicTask);

                ScheduledActionService.LaunchForTest(name, TimeSpan.FromSeconds(1));
            }
            catch (InvalidOperationException ioe)
            {


            }


        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string name = "ResourceIntensiveTask";
            ResourceIntensiveTask resourceIntensiveTask = ScheduledActionService.Find(name) as ResourceIntensiveTask;
            if (resourceIntensiveTask == null)
            {
                resourceIntensiveTask = new ResourceIntensiveTask(name);
            }
            else
            {
                ScheduledActionService.Remove(name);
                resourceIntensiveTask = new ResourceIntensiveTask(name);
            }
            resourceIntensiveTask.Description = "描述我们的ResourceIntensiveTask后台任务是干什么的";
            try
            {
                ScheduledActionService.Add(resourceIntensiveTask);
                ScheduledActionService.LaunchForTest(name, TimeSpan.FromSeconds(1));
            }
            catch (InvalidOperationException ioe)
            {


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