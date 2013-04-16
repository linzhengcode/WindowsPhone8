using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CacheDemo.Resources;
using System.Diagnostics;
using Microsoft.Phone.Info;

namespace CacheDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        List<byte[]> memory;
        Cache c;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();
            memory = new List<byte[]>();
            c = new Cache(20);
            Debug.WriteLine("当前使用的内存" + DeviceStatus.ApplicationPeakMemoryUsage / (1024 * 1024) + "m");
        }
        //填充内存
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            memory.Add(new byte[1024*1024]);
            Debug.WriteLine("当前使用的内存" + DeviceStatus.ApplicationPeakMemoryUsage / (1024 * 1024) + "m");
        }
        //遍历缓存
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < c.Count; i++)
            {
                string name = c[i].Name;
            }
            Debug.WriteLine("当前使用的内存" + DeviceStatus.ApplicationPeakMemoryUsage / (1024 * 1024) + "m");
        }
        //调用Gc
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GC.Collect();
        }
    }
}