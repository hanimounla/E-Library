using System.Windows.Controls;

namespace E_Library.Views
{
    public partial class BookCollectionView : UserControl
    {
        public BookCollectionView()
        {
            InitializeComponent();
        }

        private void newBTN_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AddBook a = new AddBook();
            a.ShowDialog();

        }

        private void viewPDFBTN_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PDFViewer p = new PDFViewer(gridControl.GetFocusedRowCellValue("Location").ToString());
            p.Show();
        }

        private void deleteBTN_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int bookID = (int)gridControl.GetFocusedRowCellValue("ID");
            object o = System.Windows.Forms.MessageBox.Show("Delete Book " + bookID + "?",
                "Info",System.Windows.Forms.MessageBoxButtons.YesNo,System.Windows.Forms.MessageBoxIcon.Question);
            if ("Yes" == o.ToString())
                DataBaseTools.DeleteBook(bookID);
        }
    }
}
