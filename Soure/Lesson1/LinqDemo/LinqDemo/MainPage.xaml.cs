using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LinqDemo.Resources;
using System.Diagnostics;

namespace LinqDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        string[] myTests = new string[] { "kit", "tony","Davy","GoSon","Andy","buyk","ssky","boy" };
        int[] myTestsNum = new int[] { 1,2,3,4,5,3,34,5,6,7,9,33,2,14,5,78,3,223,56,8,96,2,3,45,3,23, };

        // 构造函数
        public MainPage()
        {
            InitializeComponent();
            //SelectTest();
            //OrderTest();

        }


        public void SelectTest()
        {
           var mytest2= myTests.Select((name) => name.ToLower());
           foreach (var item in mytest2)
           {
               Debug.WriteLine(item.ToString());
           }

        }

        public void WhereTest()
        {
            var mytest2 = myTests.Where(name=>name.StartsWith("A")).Select((name) => name.ToLower());
            foreach (var item in mytest2)
            {
                Debug.WriteLine(item.ToString());
            }
        }

        public void OrderTest()
        {
            var mytest2 = myTests.Where(name => name.Length > 2).Select((name) => name.ToLower()).OrderBy(name => name.Substring(0, 1));
            foreach (var item in mytest2)
            {
                Debug.WriteLine(item.ToString());
            }
        }

        public void MaxTest()
        {
            var max = (from myitem in myTestsNum where myitem%2==0 select myitem).Max();
        }


    }
}