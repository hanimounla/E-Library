using System.Windows.Controls;

namespace E_Library.Views
{
    public partial class CategoryCollectionView : UserControl
    {
        public CategoryCollectionView()
        {
            InitializeComponent();
        }

        private void AppBarButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AddCategory a = new AddCategory();
            a.ShowDialog();
        }
    }
}
