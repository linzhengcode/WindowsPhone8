using Microsoft.Phone.Data.Linq.Mapping;
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
    [Table(Name = "Order")]
    [Index(Columns = "OrderID,CustomerID DESC", IsUnique = true, Name = "MultiColumnIndex")]
    public class Order : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private int _OrderID;
        [Column(IsPrimaryKey = true)]
        public int OrderID
        {
            get
            {
                return _OrderID;
            }
            set
            {
                if (_OrderID != value)
                {
                    NotifyPropertyChanging("OrderID");
                    _OrderID = value;
                    NotifyPropertyChanged("OrderID");
                }
            }
        }
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

        private string _desc;
        [Column(CanBeNull=true)]
        public string Desc
        {
            get
            {
                return _desc;
            }
            set
            {
                if (_desc != value)
                {
                    NotifyPropertyChanging("Desc");
                    _desc = value;
                    NotifyPropertyChanged("Desc");
                }
            }
        }

        [Column]
        public string CustomerID;
        private EntityRef<Customer> _Customer;
        [Association(Storage = "_Customer", ThisKey = "OrderID", OtherKey = "CustomerID")]
        public Customer Customer
        {
            get { return this._Customer.Entity; }
            set { this._Customer.Entity = value; }
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
