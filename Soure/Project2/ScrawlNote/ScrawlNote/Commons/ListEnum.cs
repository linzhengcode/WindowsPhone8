using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ScrawlNote.Commons
{
    public class ListEnum<T> : List<KeyValuePair<string, T>> where T : struct
    {
        public ListEnum()
        {
            foreach (FieldInfo info in from info in typeof(T).GetFields() where info.FieldType.Equals(typeof(T)) select info)
            {
                base.Add(new KeyValuePair<string, T>(this.Sistema(info.Name), (T)Enum.Parse(typeof(T), info.Name, false)));
            }
        }

        private string Sistema(string value)
        {
            return value.Replace("_", " ");
        }
    }
}
