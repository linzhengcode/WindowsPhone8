using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrawlNote.ViewModels
{
    public class ViewModelNewBase
    {
        public virtual void CopyViewModel()
        {
        }

        public virtual void RestoreViewModel()
        {
        }

        public bool IsAbort { get; set; }

        public bool IsDelete { get; set; }
    }
}
