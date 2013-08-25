using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_1_Demo
{
    public class OrderModel: INotifyPropertyChanged
    {
        public string _orderID;
        public string OrderID 
        { 
            get
            {
                return _orderID;
            }
            set
            {
                if (_orderID == value)
                    return;
                _orderID = value;
                NotifyPropertyChanged("OrderID");
            }
        }

        public string OrderName { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,new PropertyChangedEventArgs(propertyname));
            }
        }
    }
}
