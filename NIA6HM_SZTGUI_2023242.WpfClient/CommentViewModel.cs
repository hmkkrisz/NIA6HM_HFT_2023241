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
    public class CommentViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<Comment> Comments { get; set; }

        private Comment selectedComment;

        public Comment SelectedComment
        {
            get { return selectedComment; }
            set
            {
                if (value != null)
                {
                    selectedComment = new Comment()
                    {
                        Text = value.Text,
                        CommentId = value.CommentId
                    };
                    OnPropertyChanged();
                    (DeleteCommentCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }
        public ICommand CreateCommentCommand { get; set; }

        public ICommand DeleteCommentCommand { get; set; }

        public ICommand UpdateCommentCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }


        public CommentViewModel()
        {
            if (!IsInDesignMode)
            {
                Comments = new RestCollection<Comment>("http://localhost:58339/", "comment", "hub");
                CreateCommentCommand = new RelayCommand(() =>
                {
                    Comments.Add(new Comment()
                    {
                        Text = SelectedComment.Text
                    });
                });

                UpdateCommentCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Comments.Update(SelectedComment);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteCommentCommand = new RelayCommand(() =>
                {
                    Comments.Delete(SelectedComment.CommentId);
                },
                () =>
                {
                    return SelectedComment != null;
                });
                SelectedComment = new Comment();
            }

        }
    }
}
