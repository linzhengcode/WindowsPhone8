using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RenderingDemo.Resources;
using System.Windows.Media;

namespace RenderingDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        Point mouseLocation;
        TranslateTransform translateTransform = new TranslateTransform();
        DateTime preTime = DateTime.Now;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            CompositionTarget.Rendering += CompositionTarget_Rendering;
            this.rectangle.RenderTransform = translateTransform;
            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }

        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            var currentTime = DateTime.Now;
            double elapsedTime = (currentTime - preTime).TotalSeconds;
            preTime = currentTime;
            if (mouseLocation != null)
            {
                translateTransform.X += mouseLocation.X * elapsedTime;
                if (translateTransform.X > 410) translateTransform.X = 410;
                if (translateTransform.X < 0) translateTransform.X =0;
                translateTransform.Y += mouseLocation.Y * elapsedTime;
                if (translateTransform.Y> 530) translateTransform.Y = 530;
                if (translateTransform.Y < 0) translateTransform.Y = 0;

            }
            
        }
        
        private void Canvas_MouseMove_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            mouseLocation = e.GetPosition(this.rectangle);
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