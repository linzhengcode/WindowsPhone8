using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TreeDemo.Resources;
using System.Windows.Media;
using System.Diagnostics;

namespace TreeDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        // 构造函数
        public MainPage()
        {
            InitializeComponent();
            for (int i = 0; i < 30; i++)
            {
                listbox1.Items.Add("项目" + i);
            }

            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
          //  GetChildType(stackpanel);
        }

        public void GetChildType(DependencyObject dob)
        {
            int count = VisualTreeHelper.GetChildrenCount(dob);
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    var child = VisualTreeHelper.GetChild(dob, i);
                    Debug.WriteLine("类型是" + child.GetType() + "子节点位置" + i);
                    GetChildType(child);
                }
            }
        }

        private void listbox1_MouseMove_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ScrollViewer scrollViewer = FindChildOfType<ScrollViewer>(listbox1);
            if (scrollViewer != null)
            {
                if (scrollViewer.VerticalOffset+1 >= scrollViewer.ScrollableHeight)
                {
                    for (int i = 0; i < 30; i++)
                    {
                        listbox1.Items.Add("刷新项目" + i);
                    }
                }
            }
        }

        public T FindChildOfType<T>(DependencyObject root) where T : class
        {
            var queue = new Queue<DependencyObject>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(current); i++)
                {
                    var child = VisualTreeHelper.GetChild(current, i);
                    var typeChild = child as T;
                    if (typeChild != null)
                    {
                        return typeChild;
                    }
                    queue.Enqueue(child);
                }
            }
            return null;
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