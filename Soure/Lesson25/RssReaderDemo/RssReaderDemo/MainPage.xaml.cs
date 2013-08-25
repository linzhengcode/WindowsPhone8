using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RssReaderDemo.Resources;
using RssReaderDemo.Services;
using GalaSoft.MvvmLight.Messaging;
using RssReaderDemo.Models;
using GalaSoft.MvvmLight.Ioc;

namespace RssReaderDemo
{
    public partial class MainPage : IDialogService
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public void ShowError(string errorMessage, string title, string buttonText)
        {
            MessageBox.Show(errorMessage, title, MessageBoxButton.OK);
        }

        public void ShowError(Exception error, string title, string buttonText)
        {
            MessageBox.Show(error.Message, title, MessageBoxButton.OK);
        }

        public void ShowMessage(string message, string title, string buttonText)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Messenger.Default.Unregister<StatusMessage>(
                this, HandleStatusMessage);

            SimpleIoc.Default.Unregister<IDialogService>();

            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Messenger.Default.Register<StatusMessage>(
                this, HandleStatusMessage);

            SimpleIoc.Default.Register<IDialogService>(() => this);

            base.OnNavigatedTo(e);
        }

        private void HandleStatusMessage(StatusMessage msg)
        {
            Status.Message = msg.Status;
            Status.Show(msg.TimeoutMilliseconds);
        }
    }
}