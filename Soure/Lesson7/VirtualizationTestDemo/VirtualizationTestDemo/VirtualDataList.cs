using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace VirtualizationTestDemo
{
    class VirtualDataList : IList
    {
        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();

            //for (int i = 0; i < Count; ++i)
            //{
            //    yield return new Song();
            //}
        }

        #endregion

        #region IList Members

        public int Add(object value)
        {
            throw new NotImplementedException();
        }

        public bool Contains(object value)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(object value)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        public bool IsFixedSize
        {
            get { throw new NotImplementedException(); }
        }

        public void Remove(object value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        object IList.this[int index]
        {
            get
            {
                Debug.WriteLine("当前加载的数据 data" + index.ToString());
                return new Data { Name = "data " + index.ToString()};
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region ICollection Members

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public bool IsSynchronized
        {
            get { throw new NotImplementedException(); }
        }

        public object SyncRoot
        {
            get { throw new NotImplementedException(); }
        }

        public int Count
        {
            get
            {
                return 1000;
            }
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }
        #endregion
    }

    public class Data 
    {
        public string Name { get; set; }
    }
}
