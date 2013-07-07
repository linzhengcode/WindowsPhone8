using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SQLServerDemo.Resources;

namespace SQLServerDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        int i=0;
        MyDataContext MyDataContext;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            MyDataContext = new MyDataContext("Data Source=isostore:/MyDataContext.sdf");

            // 用于本地化 ApplicationBar 的示例代码
            //BuildLocalizedApplicationBar();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            i++;
            var list = (from customer2 in MyDataContext.CustomerTable where customer2.CustomerID == "CustomerID" + i select customer2).ToList();

            if (list.Count > 0)
            {
                MessageBox.Show("数据已经存在");
                return;
            }


            Customer customer = new Customer { CustomerID = "CustomerID" + i, ID = i, Name = "CustomerName" + i };
            MyDataContext.CustomerTable.InsertOnSubmit(customer);

            Order order1 = new Order { Customer = customer, Name = "order1_" + i, OrderID = i, CustomerID = "CustomerID1" + i, Desc ="desc"};
            MyDataContext.OrderTable.InsertOnSubmit(order1);
            Order order2 = new Order { Customer = customer, Name = "order2_" + i, OrderID = i, CustomerID = "CustomerID2" + i, Desc = "desc" };
            MyDataContext.OrderTable.InsertOnSubmit(order2);

            MyDataContext.SubmitChanges();
            MessageBox.Show("成功插入");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var list = (from order in MyDataContext.OrderTable select order).ToList();
            listbox.ItemsSource = list;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var list = (from order in MyDataContext.OrderTable select order).ToList();
            if(list.Count>0)
            {
                MyDataContext.OrderTable.DeleteOnSubmit(list[0]);
                MyDataContext.SubmitChanges();
                MessageBox.Show("删除成功");
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var list = (from order in MyDataContext.OrderTable select order).ToList();
            if (list.Count > 0)
            {
                Order order = list[0];
                order.Desc = "修改";
                MyDataContext.SubmitChanges();
                MessageBox.Show("修改成功");
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