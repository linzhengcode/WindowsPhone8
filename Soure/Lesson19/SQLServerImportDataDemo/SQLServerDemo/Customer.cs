using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLServerDemo
{
    [Table(Name = "Customer")]
    public class Customer : INotifyPropertyChanged, INotifyPropertyChanging
    {
        [Column(IsPrimaryKey=true)]
        public string CustomerID { get; set; }

        public int ID { get; set; }

        private string _Name;
        [Column(IsPrimaryKey = true)]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (_Name != value)
                {
                    NotifyPropertyChanging("Name");
                    _Name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        private EntitySet<Order> _Orders;
        [Association(Storage = "_Orders", OtherKey = "OrderID", ThisKey = "CustomerID")]
        public EntitySet<Order> Orders
        {
            get { return this._Orders; }
            set { this._Orders.Assign(value); }
        }
        private void add(Order order)
        {
            order.Customer = this;
        }

        private void removed(Order order)
        {
            order.Customer = null;
        }

        public Customer()
        {
            _Orders = new EntitySet<Order>(add, removed);
        }

        
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }
}
