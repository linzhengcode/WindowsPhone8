using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ScrawlNote.Models;
using System.Windows.Shapes;
using System.Windows.Media;

namespace ScrawlNote.Controls
{
    public partial class ShowDrawingsControl : UserControl
    {
        public static readonly DependencyProperty DataSourceProperty = DependencyProperty.Register("DataSource", typeof(object), typeof(ShowDrawingsControl), new PropertyMetadata(null, new PropertyChangedCallback(ShowDrawingsControl.DataSourceChanged)));

        public ShowDrawingsControl()
        {
            this.InitializeComponent();
        }

        private static void DataSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ShowDrawingsControl drawings = sender as ShowDrawingsControl;
            if ((drawings.DataSource != null) && (drawings.DataSource is List<DrawModel>))
            {
                drawings.Draw();
            }
        }
        private double Scale = 0.3;
        internal void Draw()
        {
            this.panel.Children.Clear();
            List<DrawModel> drawModels = DataSource as List<DrawModel>;
            foreach (DrawModel drawModel in drawModels)
            {
                Canvas canvas = new Canvas();
                canvas.Height = drawModel.Height * Scale;
                canvas.Width = drawModel.Width * Scale;

                foreach (LineModel line in drawModel.ListLine)
                {
                    if (line.IsVisible)
                    {
                        Line line2 = new Line
                        {
                            X1 = line.X1 * Scale,
                            Y1 = line.Y1 * Scale,
                            X2 = line.X2 * Scale,
                            Y2 = line.Y2 * Scale,
                            Stroke = new SolidColorBrush(Colors.Black),
                            StrokeThickness = 0.3,
                            StrokeLineJoin = PenLineJoin.Round,
                            StrokeStartLineCap = PenLineCap.Round,
                            StrokeEndLineCap = PenLineCap.Round
                        };
                        canvas.Children.Add(line2);
                    }
                }
                this.panel.Children.Add(canvas);

            }
        }

        public object DataSource
        {
            get
            {
                return base.GetValue(DataSourceProperty);
            }
            set
            {
                base.SetValue(DataSourceProperty, value);
            }
        }
    }
}
