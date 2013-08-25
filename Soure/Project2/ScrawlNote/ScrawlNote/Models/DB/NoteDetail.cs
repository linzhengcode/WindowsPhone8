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
    public class NoteDetail
    {
        public NoteDetail()
        {
            _draw = SerializeListDrawSub();
        }

        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        private int? _parentId = null;
        [Column(Storage = "ParentId")]
        public int? ParentId
        {
            get
            {
                return _parentId;
            }
            set
            {
                _parentId = value;
            }

        }

        private EntityRef<Note> parentRef = new EntityRef<Note>();
       [Association(Name = "FK_Note_NoteDetail", Storage = "parentRef", ThisKey = "ParentId", OtherKey = "Id",IsForeignKey=true)]
        public Note Parent
        {
            get
            {
                return parentRef.Entity;
            }
            set
            {
                Note note = parentRef.Entity;
                if (note != value || parentRef.HasLoadedOrAssignedValue)
                {
                    parentRef.Entity = value;
                    if (value == null)
                    {
                        ParentId = null;
                    }
                    else
                    {
                        value.Body.Add(this);
                        ParentId = new int?(value.Id);
                    }

                }
            }
        }

       [Column]
       public string Text { get; set; }

       private string _draw = "";
       [Column(DbType = "ntext",UpdateCheck= UpdateCheck.Never)]
       public string Draw {
           get
           {
               return _draw;
           }
           set
           {
               _draw = value;
               if (ListPageDraw == null)
               {
                   ListPageDraw = new List<DrawModel>();
               }
               else
               {
                   ListPageDraw.Clear();
               }
               if (_draw != "")
               {
                   string[] strArray= _draw.Split('#');
                   if (strArray != null && strArray.Length > 0)
                   {
                       for (int i = 0; i < strArray.Length; i++)
                       {
                           if (strArray[i] != "")
                           {
                               ListPageDraw.Add(new DrawModel(strArray[i]));
                           }
                       }
                   }
               }
           }
       }

       private List<DrawModel> _listPageDraw;
       public List<DrawModel> ListPageDraw
       {
           get
           {
               return _listPageDraw;
           }
           set
           {
               _listPageDraw = value;
           }
       }

       public void SerializeListDraw()
       {
           this._draw = this.SerializeListDrawSub();
       }

       private string SerializeListDrawSub()
       {
           if (ListPageDraw != null && ListPageDraw.Count > 0)
           {
               StringBuilder str = new StringBuilder();
               string fomat = "{0}#";
               ListPageDraw.ForEach(item=>str.AppendFormat(fomat,new object[]{item.Serialize()}));
               return str.ToString();
           }
           return "";
       }
    }
}
