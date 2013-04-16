using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using EvenDemo.Resources;

namespace EvenDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        NumberTask numberTask;
        // 构造函数
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            numberTask = new NumberTask();
            
            //EvenDemo.NumberTask.MyDelegate DelegateObject = new EvenDemo.NumberTask.MyDelegate(DelegateFunction);
            //numberTask.GetNumber = DelegateObject;
            


            numberTask.StateChangedEvent += OnStateChanged;
            numberTask.StateChangedEvent += OnStateChanged2;
            numberTask.StartNumberTask(1000);
            
        }
        //4
        public void OnStateChanged(object sender, NumberEventArgs args)
        {
            NumberTask task = sender as NumberTask;

            textBlock1.Text = args.State.ToString() + task.TaskString;
        }

        public void OnStateChanged2(object sender, NumberEventArgs args)
        {
            MessageBox.Show(args.TimesTemp.ToLongTimeString());
        }

        public void DelegateFunction(int PassValue)
        {
            textBlock1.Text = PassValue.ToString();
        }
    }
}