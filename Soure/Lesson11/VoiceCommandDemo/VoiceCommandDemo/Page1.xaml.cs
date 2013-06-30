using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace VoiceCommandDemo
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.Keys.Contains("voiceCommandName"))
            {
                string command = NavigationContext.QueryString["voiceCommandName"];
                if (command == "Command1")
                {
                    MessageBox.Show("Command1");
                }
                else if (command == "Command2")
                {
                    string number = NavigationContext.QueryString["number"];
                    string reco = NavigationContext.QueryString["reco"];
                    MessageBox.Show("Command2"+number+reco);
                }
            }
            base.OnNavigatedTo(e);
        }
    }
}