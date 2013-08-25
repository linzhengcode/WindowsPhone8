using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using ScrawlNote.Models;
using System.Windows.Shapes;
using System.Windows.Input;

namespace ScrawlNote.Controls
{
    public partial class DrawControl : UserControl
    {
        private SolidColorBrush _brush;
        private DrawModel _currentPage;
        private Line _linetemp;
        private double _strokeThickness = 7.0;

        public static readonly DependencyProperty CurrentPageCountProperty = DependencyProperty.Register("CurrentPageCount", typeof(int), typeof(DrawControl), new PropertyMetadata(0));

        public int CurrentPageCount
        {
            get
            {
                return (int)base.GetValue(CurrentPageCountProperty);
            }
            set
            {
                base.SetValue(CurrentPageCountProperty, value);
            }
        }
        private List<DrawModel> ListPage { get; set; }

        public DrawControl()
        {
            this.InitializeComponent();
            this._brush = new SolidColorBrush(Colors.Black);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.InizializeFirstPage();
        }

        public void InizializeFirstPage()
        {
            if (this.ListPage == null)
            {
                this.ListPage = new List<DrawModel>();
                this._currentPage = new DrawModel(this.ListPage.Count, this.panel.ActualWidth, this.panel.ActualHeight);
                this.ListPage.Add(this._currentPage);
                this.panel.Children.Clear();
                this.CurrentPageCount = 1;
            }
            else if (this.ListPage.Count > 0)
            {
                this._currentPage = this.ListPage[0];
                this.CurrentPageCount = 1;
                this.ReloadData();
            }
            else if (this.ListPage.Count == 0)
            {
                this._currentPage = new DrawModel(this.ListPage.Count, this.panel.ActualWidth, this.panel.ActualHeight);
                this.ListPage.Add(this._currentPage);
                this.panel.Children.Clear();
                this.CurrentPageCount = 1;
            }
        }

        private void ReloadData()
        {
            try
            {
                if (this._currentPage != null)
                {
                    foreach (LineModel line in this._currentPage.ListLine)
                    {
                        if (line.IsVisible)
                        {
                            Line line2 = new Line
                            {
                                X1 = line.X1,
                                Y1 = line.Y1,
                                X2 = line.X2,
                                Y2 = line.Y2,
                                Stroke = this._brush,
                                StrokeThickness = this._strokeThickness,
                                StrokeLineJoin = PenLineJoin.Round,
                                StrokeStartLineCap = PenLineCap.Round,
                                StrokeEndLineCap = PenLineCap.Round
                            };
                            this.panel.Children.Add(line2);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }



        //画图
        private bool _isManipulation;
        private void UC_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            if (e.OriginalSource.Equals(this.panel) || (e.OriginalSource is Line))
            {
                this._linetemp = null;
                e.ManipulationContainer = this.panel;
                this._currentPage.ResetInvisiblePoint();
                this._currentPage.ProgressIndex++;
                Line line = new Line
                {
                    X1 = e.ManipulationOrigin.X,
                    Y1 = e.ManipulationOrigin.Y,
                    Stroke = this._brush,
                    StrokeThickness = this._strokeThickness,
                    StrokeLineJoin = PenLineJoin.Round,
                    StrokeStartLineCap = PenLineCap.Round,
                    StrokeEndLineCap = PenLineCap.Round
                };
                this._linetemp = line;
                this._isManipulation = true;//画图开始的标志
            }
        }

        private void UC_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            if (this._isManipulation)
            {
                //控制边界
                bool flag = false;
                if (e.ManipulationOrigin.X < 0.0)
                {
                    flag = true;
                }
                if (e.ManipulationOrigin.Y < 0.0)
                {
                    flag = true;
                }
                if (e.ManipulationOrigin.X > e.ManipulationContainer.RenderSize.Width)
                {
                    flag = true;
                }
                if (e.ManipulationOrigin.Y > e.ManipulationContainer.RenderSize.Height)
                {
                    flag = true;
                }
                if (flag)
                {
                    this._linetemp = null;
                    e.Handled = true;
                }
                else if (this._linetemp == null)
                {
                    Line line2 = new Line
                    {
                        X1 = e.ManipulationOrigin.X,
                        Y1 = e.ManipulationOrigin.Y,
                        Stroke = this._brush,
                        StrokeThickness = this._strokeThickness,
                        StrokeLineJoin = PenLineJoin.Round,
                        StrokeStartLineCap = PenLineCap.Round,
                        StrokeEndLineCap = PenLineCap.Round
                    };
                    this._linetemp = line2;
                }
                else
                {
                    this._linetemp.X2 = e.ManipulationOrigin.X;
                    this._linetemp.Y2 = e.ManipulationOrigin.Y;
                    this._currentPage.ListLine.Add(new LineModel(this._linetemp.X1, this._linetemp.Y1, this._linetemp.X2, this._linetemp.Y2, this._currentPage.ProgressIndex));
                    this.panel.Children.Add(this._linetemp);
                    this._linetemp = null;
                    Line line = new Line
                    {
                        X1 = e.ManipulationOrigin.X,
                        Y1 = e.ManipulationOrigin.Y,
                        Stroke = this._brush,
                        StrokeThickness = this._strokeThickness,
                        StrokeLineJoin = PenLineJoin.Round,
                        StrokeStartLineCap = PenLineCap.Round,
                        StrokeEndLineCap = PenLineCap.Round
                    };
                    this._linetemp = line;
                }
            }
        }

        private void UserControl_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            this._isManipulation = false;
        }


        public IEnumerable<DrawModel> GetListPage()
        {
            return this.ListPage.Where<DrawModel>(delegate(DrawModel item)
            {
                item.ResetInvisiblePoint();
                return (item.ListLine.Count > 0);
            });
        }

        public void SetListPage(List<DrawModel> lst)
        {
            if (lst != null)
            {
                if (this.ListPage != null)
                {
                    this.ListPage.Clear();
                }
                this.ListPage = new List<DrawModel>();
                this.ListPage.AddRange(from item in lst select item.Clone());
                this.InizializeFirstPage();
            }
        }

        public void NextPage()
        {
            if ((this._currentPage != null) && (this._currentPage.ListLine.Count != 0))
            {
                int index = this.ListPage.IndexOf(this._currentPage);
                this._linetemp = null;
                if (index < (this.ListPage.Count - 1))
                {
                    this._currentPage = this.ListPage[index + 1];
                    this.panel.Children.Clear();
                    this.ReloadData();
                }
                else if (index >= (this.ListPage.Count - 1))
                {
                    this._currentPage = new DrawModel(this.ListPage.Count, this.panel.ActualWidth, this.panel.ActualHeight);
                    this.ListPage.Add(this._currentPage);
                    this.panel.Children.Clear();
                    this.ReloadData();
                }
                if (this._currentPage != null)
                {
                    this.CurrentPageCount = this.ListPage.IndexOf(this._currentPage) + 1;
                }
            }
        }

        public void PreviousPage()
        {
            if (this._currentPage != null)
            {
                int index = this.ListPage.IndexOf(this._currentPage);
                this._linetemp = null;
                if (index > 0)
                {
                    this._currentPage = this.ListPage[index - 1];
                    this.panel.Children.Clear();
                    this.ReloadData();
                }
                if (this._currentPage != null)
                {
                    this.CurrentPageCount = this.ListPage.IndexOf(this._currentPage) + 1;
                }
            }
        }

        public void Clear()
        {
            this._currentPage.ListLine.Clear();
            this.panel.Children.Clear();
        }



        public void Rendo()
        {
            if (!this._isManipulation)
            {
                LineModel firstInvisible = this._currentPage.GetFirstInvisible();
                if (firstInvisible != null)
                {
                    foreach (LineModel line2 in this._currentPage.GetByIndex(firstInvisible.Index))
                    {
                        line2.IsVisible = true;
                        Line line3 = new Line
                        {
                            X1 = line2.X1,
                            Y1 = line2.Y1,
                            X2 = line2.X2,
                            Y2 = line2.Y2,
                            Stroke = this._brush,
                            StrokeThickness = this._strokeThickness,
                            StrokeLineJoin = PenLineJoin.Round,
                            StrokeStartLineCap = PenLineCap.Round,
                            StrokeEndLineCap = PenLineCap.Round
                        };
                        this.panel.Children.Insert(this._currentPage.ListLine.IndexOf(line2), line3);
                    }
                }
            }
        }

        public void Undo()
        {
            if (!this._isManipulation)
            {
                LineModel lastVisible = this._currentPage.GetLastVisible();
                if (lastVisible != null)
                {
                    IEnumerable<LineModel> byIndex = this._currentPage.GetByIndex(lastVisible.Index);
                    if (byIndex.Count<LineModel>() > 0)
                    {
                        LineModel[] lineArray = byIndex.ToArray<LineModel>();
                        for (int i = lineArray.Length - 1; i >= 0; i--)
                        {
                            int index = this._currentPage.ListLine.IndexOf(lineArray[i]);
                            lineArray[i].IsVisible = false;
                            this.panel.Children.RemoveAt(index);
                        }
                    }
                }
            }
        }

 

    }
}
