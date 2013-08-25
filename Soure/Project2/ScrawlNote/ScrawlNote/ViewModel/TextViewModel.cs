using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrawlNote.ViewModels
{
    public class TextViewModel : ViewModelNewBase
    {
        private string _orgText = string.Empty;
        private string _text = string.Empty;

        public override void CopyViewModel()
        {
            this.OriginalText = this.CurrentText;
        }

        public override void RestoreViewModel()
        {
            this.CurrentText = this.OriginalText;
        }

        public string CurrentText
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
            }
        }

        public string OriginalText
        {
            get
            {
                return this._orgText;
            }
            set
            {
                this._orgText = value;
            }
        }
    }
}
