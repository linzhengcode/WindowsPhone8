using ScrawlNote.Models.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrawlNote.ViewModels
{
    public class MainViewModel
    {     
        private string _filter;
        private DBContext db;

        public MainViewModel()
            : this(string.Empty, string.Empty)
        {
        }

        public MainViewModel(string search, string search2)
        {
            this._filter = string.Empty;
            this.db = new DBContext(DBContext.DBConnectString);
            this.FavoriteNotes = new ObservableCollection<Note>();
            this.NotFavoriteNotes = new ObservableCollection<Note>();
            this.Read(true, search);
            this.Read(false, search2);
        }

        public void Read(bool favorite)
        {
            this.Read(favorite, this._filter);
        }

        public void Read(bool favorite, string filter)
        {
            this._filter = filter;
            if (this.db.Notes.Count<Note>() <= 0)
            {
                if (favorite)
                {
                    this.FavoriteNotes.Clear();
                }
                else
                {
                    this.NotFavoriteNotes.Clear();
                }
            }
            else
            {
                IQueryable<Note> queryable = null;
                if (string.IsNullOrEmpty(this._filter))
                {
                    queryable = from m in this.db.Notes.Cast<Note>()
                                where m.Favorite == favorite
                                select m;
                }
                else
                {
                    queryable = from m in this.db.Notes.Cast<Note>()
                                where (m.Favorite == favorite) && SqlMethods.Like(m.Title, string.Format("%{0}%", this._filter))
                                select m;
                }
                if (favorite)
                {
                    this.FavoriteNotes.Clear();
                    foreach (Note note in queryable)
                    {
                        this.FavoriteNotes.Add(note);
                    }
                }
                else
                {
                    this.NotFavoriteNotes.Clear();
                    foreach (Note note2 in queryable)
                    {
                        this.NotFavoriteNotes.Add(note2);
                    }
                }
            }
        }

        private ObservableCollection<Note> _favoriteNotes;
        public ObservableCollection<Note> FavoriteNotes
        {
            get
            {
                return this._favoriteNotes;
            }
            set
            {
                this._favoriteNotes = value;
            }
        }
        private ObservableCollection<Note> _notFavoriteNotes;
        public ObservableCollection<Note> NotFavoriteNotes
        {
            get
            {
                return this._notFavoriteNotes;
            }
            set
            {
                this._notFavoriteNotes = value;
            }
        }
    }
}
