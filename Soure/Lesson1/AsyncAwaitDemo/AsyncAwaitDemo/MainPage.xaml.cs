using System.Threading.Tasks;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Diagnostics;


namespace AsyncAwaitDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        // 同步调用
        private void btSync_Click(object sender, RoutedEventArgs e)
        {
            // 创建一个任务
            var someTask = Task<int>.Factory.StartNew(() => LongTimeFun(1, 2));
            // 该任务的运行将会一直阻塞UI线程
            MessageBox.Show("Result: " + someTask.Result.ToString());
        }

        // 使用任务等待实现异步任务
        private async void btAsync2_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("处理同步的事情");
            // 创建一个任务
            var someTask = Task<int>.Factory.StartNew(() => LongTimeFun(1, 2));
            // 等待任务，任务不会占用UI线程
            await someTask;
            Debug.WriteLine("任务返回，再次回到调用方的线程");
            MessageBox.Show("Result: " + someTask.Result.ToString());
        }
        // 模拟耗时任务
        private int LongTimeFun(int a, int b)
        {
            System.Threading.Thread.Sleep(10000);
            return a + b;
        }
    }
}