using System.Windows.Controls;

namespace E_Library.Views
{
    public partial class PublisherCollectionView : UserControl
    {
        public PublisherCollectionView()
        {
            InitializeComponent();
        }

        private void AppBarButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AddPublisher a = new AddPublisher();
            a.ShowDialog();
            
        }
    }
}
