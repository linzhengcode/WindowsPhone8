using ScrawlNote.Commons;
using ScrawlNote.Models;
using ScrawlNote.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ScrawlNote.ViewModels
{
    public class AddEditNoteViewModel
    {
        private DBContext db;

        public AddEditNoteViewModel()
            : this(0)
        {
        }

        public AddEditNoteViewModel(int id)
        {
            this.db = new DBContext(DBContext.DBConnectString);
            this.ListColor = new ListEnum<ColorModel>();
            this.ListViewModels = new List<ViewModelNewBase>();
            if (id == 0)
            {
                this.StateModel = StateModel.New;
                this.Note = new Note();
            }
            else if (id > 0)
            {
                this.StateModel = StateModel.Update;
                IQueryable<Note> source = from m in this.db.Notes where m.Id == id select m;
                if (source.Count<Note>() > 0)
                {
                    this.Note = source.First<Note>();
                    this.ListViewModels.Clear();
                    foreach (NoteDetail dett in this.Note.Body)
                    {
                        if (!string.IsNullOrEmpty(dett.Text))
                        {
                            TextViewModel item = new TextViewModel
                            {
                                CurrentText = dett.Text
                            };
                            this.ListViewModels.Add(item);
                        }
                        else
                        {
                            DrawViewModel draw = new DrawViewModel
                            {
                                CurrentList = dett.ListPageDraw
                            };
                            this.ListViewModels.Add(draw);
                        }
                    }

                    this.IndexColor = this.ListColor.Where<KeyValuePair<string, ColorModel>>(m => ((ColorModel)m.Value) == this.Note.Color)
                        .Select<KeyValuePair<string, ColorModel>, int>(m => this.ListColor.IndexOf(m)).First<int>();
                }
            }
        }

        public void Delete()
        {
            if (this.StateModel == StateModel.Update)
            {
                this.db.NoteDetails.DeleteAllOnSubmit<NoteDetail>(this.Note.Body);
                this.db.Notes.DeleteOnSubmit(this.Note);
                this.db.SubmitChanges();
            }
        }

        public bool Save()
        {
            try
            {
                if (string.IsNullOrEmpty(this.Note.Title) || string.IsNullOrWhiteSpace(this.Note.Title))
                {
                    MessageBox.Show("请填写标题");
                    return false;
                }
                this.Note.Color = this.ListColor[this.IndexColor].Value;
                this.Note.Body.Clear();
                foreach (ViewModelNewBase base2 in this.ListViewModels)
                {
                    NoteDetail dett = new NoteDetail
                    {
                        Parent = this.Note
                    };
                    this.Note.Body.Add(dett);
                    if (base2 is TextViewModel)
                    {
                        dett.Text = (base2 as TextViewModel).CurrentText;
                    }
                    else if (base2 is DrawViewModel)
                    {
                        dett.ListPageDraw = (base2 as DrawViewModel).CurrentList;
                        dett.SerializeListDraw();
                    }
                }
                if (this.StateModel == StateModel.New)
                {
                    this.db.Notes.InsertOnSubmit(this.Note);
                }
                this.db.SubmitChanges();
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return false;
            }
        }

        public int IndexColor { get; set; }

        public ListEnum<ColorModel> ListColor { get; private set; }

        public List<ViewModelNewBase> ListViewModels { get; set; }

        public Note Note { get; set; }

        public StateModel StateModel { get; private set; }
    }
}