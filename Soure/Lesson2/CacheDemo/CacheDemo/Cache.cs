using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheDemo
{
    class Cache
    {
        // 使用一个字典的数据结构来存放缓存
        static Dictionary<int, WeakReference> _cache;

        public Cache(int count)
        {
            _cache = new Dictionary<int, WeakReference>();
            // 添加数据
            for (int i = 0; i < count; i++)
            {
                _cache.Add(i, new WeakReference(new Data(i), false));
            }
        }

        //缓存的数量
        public int Count
        {
            get
            {
                return _cache.Count;
            }
        }

        //访问缓存的数据，如果缓存的数据被回收了，则重新创建
        public Data this[int index]
        {
            get
            {
                //把缓存的数据读取出来
                Data d = _cache[index].Target as Data;
                if (d == null)
                {
                    Debug.WriteLine("缓存被回收了 "+index.ToString());
                    //重新初始化数据
                    d = new Data(index);
                }
                else
                {
                    Debug.WriteLine("使用了缓存 "+index.ToString());
                }
                return d;
            }
        }
    }
    //模拟数据
    public class Data
    {
        private byte[] _data;
        private string _name;

        public Data(int size)
        {
            _data = new byte[size * 500];
            _name = size.ToString();
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }
    }
}




