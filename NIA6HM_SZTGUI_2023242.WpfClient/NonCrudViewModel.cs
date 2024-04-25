using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using NIA6HM_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Net.Http;


namespace NIA6HM_SZTGUI_2023242.WpfClient
{
    public class NonCrudViewModel : ObservableRecipient
    {

        private string _inputCommentId;
        private string _result;

        public NonCrudViewModel()
        {
            GetAuthorStatistics = new RelayCommand(AuthorStatistics);
            GetAvgLikesPerCategory = new RelayCommand(AvgLikesPerCategory);
            GetMostLikedAuthor = new RelayCommand(MostLikedAuthor);
            GetTop3MostCommentedArticle = new RelayCommand(Top3MostCommentedArticle);
            GetCommentsForArticle = new RelayCommand(CommentsForArticle);
        }

        public ICommand GetAuthorStatistics { get; }
        public ICommand GetAvgLikesPerCategory { get; }
        public ICommand GetMostLikedAuthor { get; }
        public ICommand GetTop3MostCommentedArticle { get; }
        public ICommand GetCommentsForArticle { get; }


        public string InputCommentId
        {
            get => _inputCommentId;
            set => SetProperty(ref _inputCommentId, value);
        }
        public string Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        private async void GetResultAsync(string url)
        {
            if (!IsInDesignMode)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        var response = await client.GetAsync("http://localhost:58339/" + url);
                        response.EnsureSuccessStatusCode();
                        Result = await response.Content.ReadAsStringAsync();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while fetching data: " + ex.Message);
                }
            }
        }

        private void AuthorStatistics()
        {
            GetResultAsync("Stat/AuthorStatistics");
        }

        private void AvgLikesPerCategory()
        {
            GetResultAsync("Stat/AvgLikesPerCategory");
        }

        private void MostLikedAuthor()
        {
            GetResultAsync("Stat/GetMostLikedAuthor");
        }

        private void Top3MostCommentedArticle()
        {
            GetResultAsync("Stat/Top3MostCommentedArticle");
        }

        private void CommentsForArticle()
        {
            GetResultAsync("Stat/GetCommentsForArticle/" + InputCommentId);
        }
    }
}
