using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrawlNote.Models.DB
{
    public class DBContext: DataContext
    {
        public static string DBConnectString = "data Source=isostore:/ScrawlNote.sdf";

        public DBContext(string str) : base(str) { }

        public Table<Note> Notes;
        public Table<NoteDetail> NoteDetails;
    }
}
