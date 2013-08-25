using Microsoft.Expression.Interactivity.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MVVM_1_Demo
{
    public class OrderViewModel
    {
        public string Title { get; set; }
      //  public List<OrderModel> OrderModels { get; set; }

        public ObservableCollection<OrderModel> OrderModels { get; set; }

        public ICommand TestCommand { get; set; }

        public int TileIndex { get; set; }

        public OrderViewModel()
        {
            TestCommand = new ActionCommand(parm =>
                {
                    MessageBox.Show("s2:" + parm + "  OrderModels.Count" + OrderModels.Count.ToString());
               
                });
            TileIndex = 1;
            Title = "测试标题";
           // OrderModels = new List<OrderModel>();
            OrderModels = new ObservableCollection<OrderModel>();
            OrderModels.Add(new OrderModel { OrderID = "OrderID1", OrderName = "OrderName1" });
            OrderModels.Add(new OrderModel { OrderID = "OrderID2", OrderName = "OrderName2" });
            OrderModels.Add(new OrderModel { OrderID = "OrderID3", OrderName = "OrderName3" });

       

        }


        public void AddData()
        {
            Task.Factory.StartNew(() =>
            {
                Task.Delay(3000);
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    //OrderModels.Add(new OrderModel { OrderID = "OrderID4", OrderName = "OrderName4" });
                    //OrderModels.Add(new OrderModel { OrderID = "OrderID5", OrderName = "OrderName5" });
                    //OrderModels.Add(new OrderModel { OrderID = "OrderID6", OrderName = "OrderName6" });
                    foreach (var item in OrderModels)
                    {
                        item.OrderID = "修改的OrderID";
                    }
                });
            });

        }


    }
}
