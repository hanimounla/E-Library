using DevExpress.Xpf.Core;
using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace E_Library
{
    /// <summary>
    /// Interaction logic for BooksWindow.xaml
    /// </summary>
    public partial class BooksWindow : DXWindow
    {
        int BookID;
        public BooksWindow()
        {
            InitializeComponent();
            progressBar.Visibility = Visibility.Hidden;
            DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e = null;
            object sender = null; ;
            GridControl_SelectedItemChanged(sender,e);
        }

        private void GridControl_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            BookID = (int)booksGrid.GetFocusedRowCellValue("ID");
            try
            {
                coverPicIE.Source = LoadImage(DataBaseTools.GetCoverPic(BookID));
            }
            catch
            {
                
            }
        }
        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        private void saveBTN_Click(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK )
                {
                    Dispatcher.Invoke(new Action(() =>
                    {
                        byte[] book = DataBaseTools.GetPDFFile(BookID);
                        string title = booksGrid.GetFocusedRowCellValue("Title").ToString();
                        File.WriteAllBytes(folderDialog.SelectedPath + "\\" + title + ".pdf", book);
                    }));
                }
            }
        }
    }
}
