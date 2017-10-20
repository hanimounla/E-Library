using DevExpress.Xpf.Core;
using System;
using System.Windows.Forms;

namespace E_Library
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class MainWindow : DXWindow
    {
        CurrentUser currentUser;
        public MainWindow(object currentUser)
        {
            this.currentUser = (CurrentUser)currentUser;
            InitializeComponent();
            //accountTile.Header = this.currentUser.getName();
        }

        private void tablesTile_Click(object sender, EventArgs e)
        {
            Tables t = new Tables(currentUser);
            t.Show();
        }

        private void searchTile_Click(object sender, EventArgs e)
        {
            Search s = new Search();
            s.Show();
        }

        private void pdfViewrTile_Click(object sender, EventArgs e)
        {
            PDFViewer p = new PDFViewer();
            p.Show();
        }
        private void booksTile_Click(object sender, EventArgs e)
        {
            BooksWindow b = new BooksWindow();
            b.Show();
        }
        private void exitTile_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void accountTile_Click(object sender, EventArgs e)
        {
            Windows.Account a = new Windows.Account(currentUser);
            a.ShowDialog();
        }
    }
}
