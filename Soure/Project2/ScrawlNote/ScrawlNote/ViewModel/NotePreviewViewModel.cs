using ScrawlNote.Models;
using ScrawlNote.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrawlNote.ViewModels
{
    public class NotePreviewViewModel : ModelBase
    {
        private Note _note;
        private DBContext db;

        public NotePreviewViewModel(int id)
        {
            db = new DBContext(DBContext.DBConnectString);
            this.Note = (from n in this.db.Notes
                         where n.Id == id
                         select n).FirstOrDefault<Note>();
        }

        public Note Note
        {
            get
            {
                return this._note;
            }
            set
            {
                this._note = value;
                this.OnPropertyChanged("Note");
            }
        }
    }
}
