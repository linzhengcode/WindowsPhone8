using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Phone.Shell;
using System.Windows.Controls.Primitives;
using Microsoft.Phone.Controls;
using System.Linq;
using System.Diagnostics;

namespace MyProgress
{
    public class ProgressIndicator : ContentControl
    {       
        private System.Windows.Shapes.Rectangle backgroundRect;
        private System.Windows.Controls.StackPanel stackPanel;
        private System.Windows.Controls.ProgressBar progressBar;
        private System.Windows.Controls.TextBlock textBlockStatus;


        private double progressBarValue = 0;//进度条的值
        private static string defaultText = "正在加载";
        private bool showLabel;
        private string labelText;

        public ProgressIndicator()
        {
            //引用控件默认的样式
            this.DefaultStyleKey = typeof(ProgressIndicator);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.backgroundRect = this.GetTemplateChild("backgroundRect") as Rectangle;
            stackPanel = this.GetTemplateChild("stackPanel") as StackPanel;
            progressBar = this.GetTemplateChild("progressBar") as ProgressBar;
            textBlockStatus = this.GetTemplateChild("textBlockStatus") as TextBlock;

            backgroundRect.Visibility = System.Windows.Visibility.Visible;
            progressBar.IsIndeterminate = true;
        }

        public bool ShowLabel
        {
            get
            {
                return this.showLabel;
            }
            set
            {
                this.showLabel = value;
            }
        }

        public double ProgressBarValue
        {
            get
            {
                return this.progressBarValue;
            }
            set
            {
                this.progressBarValue = value;
            }
        }

        public string Text
        {
            get
            {
                return labelText;
            }
            set
            {
                this.labelText = value;
                if (this.textBlockStatus != null)
                {
                    this.textBlockStatus.Text = value;
                }
            }
        }



        internal Popup ChildWindowPopup
        {
            get;
            private set;
        }

        private static PhoneApplicationFrame RootVisual
        {
            get
            {
                return Application.Current == null ? null : Application.Current.RootVisual as PhoneApplicationFrame;
            }
        }

        internal PhoneApplicationPage Page
        {
            get { return  RootVisual.GetVisualDescendants().OfType<PhoneApplicationPage>().FirstOrDefault(); }
        }


       

        public bool IsOpen 
        {
            get 
            {
                return ChildWindowPopup != null && ChildWindowPopup.IsOpen;
            } 
        }

        public void Show()
        {
            if (this.ChildWindowPopup == null)
            {
                this.ChildWindowPopup = new Popup();
                ChildWindowPopup.Child = this;
            }
            ChildWindowPopup.IsOpen = true;

            if (Page != null && Page.ApplicationBar != null)
            {
                Page.ApplicationBar.IsVisible = false;
            }
        }

        public void Hide()
        {
            if (Page != null && Page.ApplicationBar != null)
            {
                Page.ApplicationBar.IsVisible = true ;
            }
            ChildWindowPopup.IsOpen = false;
        }

    }
}

