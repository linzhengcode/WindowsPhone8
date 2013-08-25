using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using System.Linq;
using System;

namespace ScheduledTaskAgent1
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        /// <remarks>
        /// ScheduledAgent 构造函数，初始化 UnhandledException 处理程序
        /// </remarks>
        static ScheduledAgent()
        {
            // 订阅托管的异常处理程序
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                Application.Current.UnhandledException += UnhandledException;
            });
        }

        /// 出现未处理的异常时执行的代码
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // 出现未处理的异常；强行进入调试器
                Debugger.Break();
            }
        }

        /// <summary>
        /// 运行计划任务的代理
        /// </summary>
        /// <param name="task">
        /// 调用的任务
        /// </param>
        /// <remarks>
        /// 调用定期或资源密集型任务时调用此方法
        /// </remarks>
        protected override void OnInvoke(ScheduledTask task)
        {
            string taskType="";
            //TODO: 添加用于在后台执行任务的代码
            if (task is PeriodicTask)
                taskType = "PeriodicTask";
            if (task is ResourceIntensiveTask)
                taskType = "ResourceIntensiveTask";

            ShellToast shellToast = new ShellToast();
            shellToast.Title = taskType;
            shellToast.Content = "测试";
            shellToast.NavigationUri=new System.Uri("/MainPage.xaml?taskType="+taskType, System.UriKind.Relative);
            shellToast.Show();

            ShellTile shellTile=ShellTile.ActiveTiles.First();
            shellTile.Update
                (new StandardTileData{
             Count=5
                } );

            ScheduledActionService.LaunchForTest(task.Name, TimeSpan.FromSeconds(15));

            NotifyComplete(); 
        }
    }
}