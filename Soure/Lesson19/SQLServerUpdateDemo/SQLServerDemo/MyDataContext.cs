using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLServerDemo
{
    [Database(Name = "MyDataContext")]
    public class MyDataContext : DataContext
    {
        public MyDataContext(string connectionSting) : base(connectionSting) { }

        public Table<Customer> CustomerTable;
        public Table<Order> OrderTable;
        public Table<Company> CompanyTable;
    }
}
