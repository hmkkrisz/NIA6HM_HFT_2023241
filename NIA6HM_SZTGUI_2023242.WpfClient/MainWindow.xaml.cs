using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NIA6HM_SZTGUI_2023242.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AuthorEditor authorEditor;
        private ArticleEditor articleEditor;
        private CommentEditor commentEditor;
        private NonCrudWindow nonCrudWindow;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Author_Click(object sender, RoutedEventArgs e)
        {
            if (authorEditor == null || !authorEditor.IsVisible)
            {
                authorEditor = new AuthorEditor();
                authorEditor.Closed += AuthorEditor_Closed;
                authorEditor.Show();
            }
        }
        private void Article_Click(object sender, RoutedEventArgs e)
        {
            if (articleEditor == null || !articleEditor.IsVisible)
            {
                articleEditor = new ArticleEditor();
                articleEditor.Closed += ArticleEditor_Closed;
                articleEditor.Show();
            }
        }
        private void Comment_Click(object sender, RoutedEventArgs e)
        {
            if (commentEditor == null || !commentEditor.IsVisible)
            {
                commentEditor = new CommentEditor();
                commentEditor.Closed += CommentEditor_Closed;
                commentEditor.Show();
            }
        }
        private void nonCrud_Click(object sender, RoutedEventArgs e)
        {
            AuthorEditor authorEditor = new AuthorEditor();
            authorEditor.Show();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan be szeretnéd zárni az alkalmazást?", "Megerősítés", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void AuthorEditor_Closed(object sender, EventArgs e)
        {
            if (authorEditor != null)
            {
                authorEditor.Closed -= AuthorEditor_Closed;
                authorEditor = null;
            }
        }

        private void ArticleEditor_Closed(object sender, EventArgs e)
        {
            if (articleEditor != null)
            {
                articleEditor.Closed -= ArticleEditor_Closed;
                articleEditor = null;
            }
        }

        private void CommentEditor_Closed(object sender, EventArgs e)
        {
            if (commentEditor != null)
            {
                commentEditor.Closed -= CommentEditor_Closed;
                commentEditor = null;
            }
        }
        private void NonCruds_Click(object sender, RoutedEventArgs e)
        {
            if (nonCrudWindow == null || !nonCrudWindow.IsVisible)
            {
                nonCrudWindow = new NonCrudWindow();
                nonCrudWindow.Closed += NonCrudWindow_Closed;
                nonCrudWindow.Show();
            }
        }
        private void NonCrudWindow_Closed(object sender, EventArgs e)
        {
            if (nonCrudWindow != null)
            {
                nonCrudWindow.Closed -= NonCrudWindow_Closed;
                nonCrudWindow = null;
            }
        }
    }
}
