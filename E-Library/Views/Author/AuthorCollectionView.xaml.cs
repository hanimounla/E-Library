using System.Windows.Controls;

namespace E_Library.Views
{
    public partial class AuthorCollectionView : UserControl
    {
        public AuthorCollectionView()
        {
            InitializeComponent();
        }

        private void AppBarButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AddAuthor a = new AddAuthor();
            a.ShowDialog();
        }
    }
}
