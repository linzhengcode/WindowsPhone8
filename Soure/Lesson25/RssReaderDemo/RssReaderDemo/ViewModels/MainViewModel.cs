using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using RssReaderDemo.Models;
using RssReaderDemo.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RssReaderDemo.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private const string DetailsPageUrl = "/DetailsPage.xaml?item={0}";

        private readonly INavigationService _navigationService;
        private readonly IRssService _rssService;
        private RelayCommand<RssArticle> _navigateToArticleCommand;

        public IDialogService DialogService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IDialogService>();
            }
        }

        public ObservableCollection<RssArticle> Items
        {
            get;
            private set;
        }

        public RelayCommand<RssArticle> NavigateToArticleCommand
        {
            get
            {
                return _navigateToArticleCommand
                    ?? (_navigateToArticleCommand = new RelayCommand<RssArticle>(
                        article =>
                        {
                            var url = HttpUtility.UrlEncode(article.Link.AbsoluteUri);
                            SimpleIoc.Default.Register(() => article, url);
                            _navigationService.NavigateTo(
                                new Uri(string.Format(DetailsPageUrl, url), UriKind.Relative));
                        }));
            }
        }

        private bool _isRefreshing;
        private RelayCommand _refreshCommand;

        public RelayCommand RefreshCommand
        {
            get
            {
                return _refreshCommand
                    ?? (_refreshCommand = new RelayCommand(
                        async () =>
                        {
                            if (_isRefreshing)
                            {
                                return;
                            }

                            _isRefreshing = true;
                            RefreshCommand.RaiseCanExecuteChanged();

                            await Refresh();

                            _isRefreshing = false;
                            RefreshCommand.RaiseCanExecuteChanged();
                        },
                        () => !_isRefreshing));
            }
        }

        public MainViewModel(
            IRssService rssService,
            INavigationService navigationService)
        {
            _rssService = rssService;
            _navigationService = navigationService;
            Items = new ObservableCollection<RssArticle>();
        }

        public async Task Refresh()
        {
            Items.Clear();

            try
            {
                Messenger.Default.Send(new StatusMessage("开始获取文章", 0));

                var list = await _rssService.GetArticles();

                if (list.Count == 0)
                {
                    DialogService.ShowMessage(
                        "没有发现文章",
                        "没有文章",
                        "没有文章");
                }

                foreach (var item in list)
                {
                    Items.Add(item);
                }

                Messenger.Default.Send(new StatusMessage("完成", 3000));
            }
            catch (Exception ex)
            {
                Messenger.Default.Send(new StatusMessage(string.Empty, 1));

                DialogService.ShowError(
                    ex,
                    "Error when loading",
                    "Oops");
            }
        }
    }
}
