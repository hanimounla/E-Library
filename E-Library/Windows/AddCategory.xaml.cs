using System.Windows;

namespace E_Library
{
    /// <summary>
    /// Interaction logic for AddCategory.xaml
    /// </summary>
    public partial class AddCategory : DevExpress.Xpf.Core.DXWindow
    {
        public AddCategory()
        {
            InitializeComponent();
        }

        private void addBTN_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameTB.Text))
            {
                string catName = nameTB.Text;
                DataBaseTools.AddCategory(catName , descriptionTB.Text);
                System.Windows.Forms.MessageBox.Show("Category added");
                Close();
            }
            else
                System.Windows.Forms.MessageBox.Show("Enter Category Name");

        }

        private void cancelBTN_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
