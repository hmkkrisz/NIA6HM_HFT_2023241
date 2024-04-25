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

namespace NIA6HM_SZTGUI_2023242.WpfClient
{
    public class ArticleViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<Article> Articles { get; set; }

        private Article selectedArticle;

        public Article SelectedArticle
        {
            get { return selectedArticle; }
            set
            {
                if (value != null)
                {
                    selectedArticle = new Article()
                    {
                        Title = value.Title,
                        ArticleId = value.ArticleId
                    };
                    OnPropertyChanged();
                    (DeleteArticleCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }
        public ICommand CreateArticleCommand { get; set; }

        public ICommand DeleteArticleCommand { get; set; }

        public ICommand UpdateArticleCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }


        public ArticleViewModel()
        {
            if (!IsInDesignMode)
            {
                Articles = new RestCollection<Article>("http://localhost:58339/", "article", "hub");
                CreateArticleCommand = new RelayCommand(() =>
                {
                    Articles.Add(new Article()
                    {
                        Title = SelectedArticle.Title
                    });
                });

                UpdateArticleCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Articles.Update(SelectedArticle);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteArticleCommand = new RelayCommand(() =>
                {
                    Articles.Delete(SelectedArticle.ArticleId);
                },
                () =>
                {
                    return SelectedArticle != null;
                });
                SelectedArticle = new Article();
            }

        }
    }
}
