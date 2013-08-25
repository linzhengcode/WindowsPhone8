using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrawlNote.Models.DB
{
    [Table]
    public class Note : ModelBase
    {
        public Note()
        {
            bodyRef = new EntitySet<NoteDetail>(OnBodyAdded, OnBodyRemoveed);
        }

        private EntitySet<NoteDetail> bodyRef;
        [Association(Name="FK_Note_NoteDetail", Storage="bodyRef", ThisKey="Id", OtherKey="ParentId")]
        public EntitySet<NoteDetail> Body
        {
            get
            {
                return bodyRef;
            }
        }

        private void OnBodyAdded(NoteDetail noteDetail)
        {
            noteDetail.Parent = this;
        }

        private void OnBodyRemoveed(NoteDetail noteDetail)
        {
            noteDetail.Parent = null;
        }



       [Column(IsDbGenerated=true, IsPrimaryKey=true)]
        public int Id { get; set; }

       private ColorModel _color;
       [Column]
       public ColorModel Color
       {
           get
           {
               return _color;
           }
           set
           {
               _color = value;
               OnPropertyChanged("Color");
           }
       }

       private DateTime _date = DateTime.Now;
       [Column]
       public DateTime Date
       {
           get
           {
               return _date;
           }
           set
           {
               _date = value;
               OnPropertyChanged("Date");
           }
       }

       private bool _favorite = false;
       [Column]
       public bool Favorite
       {
           get
           {
               return _favorite;
           }
           set
           {
               _favorite = value;
               OnPropertyChanged("Favorite");
           }
       }

       private string _title;
       [Column]
       public string Title
       {
           get
           {
               return _title;
           }
           set
           {
               _title = value;
               OnPropertyChanged("Title");
           }
       }

    }
}
