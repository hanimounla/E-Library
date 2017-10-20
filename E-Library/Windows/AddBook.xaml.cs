using PdfiumViewer;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace E_Library
{
    /// <summary>
    /// Interaction logic for AddBook.xaml
    /// </summary>
    public partial class AddBook : DevExpress.Xpf.Core.DXWindow
    {
        public AddBook()
        {
            InitializeComponent();
            fillCategories();
            FillAuthors();
            FillPublishers();
            progressbarLBL.Visibility = Visibility.Hidden;
            progressBar.Visibility = Visibility.Hidden;
            try{DataBaseTools.conn.Open();}
            catch (Exception){}
            BookIDTB.Text = (DataBaseTools.GetLastBook("")+1) + "";
            try { DataBaseTools.conn.Close(); }
            catch (Exception) { }
        }

        private void fillCategories()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter category_data = new SqlDataAdapter("SELECT ID,Name FROM Categories", DataBaseTools.conn);
            category_data.Fill(ds, "Categories");
            categoryCB.ItemsSource = ds.Tables["Categories"].DefaultView;
        }

        private void FillAuthors()
        {
            try { authorsCB.Items.Clear(); } catch { };
            DataSet ds = new DataSet();
            SqlDataAdapter authors_data = new SqlDataAdapter("SELECT ID,Name FROM Authors", DataBaseTools.conn);
            authors_data.Fill(ds, "Authors");
            authorsCB.ItemsSource = ds.Tables["Authors"].DefaultView;
        }

        private void FillPublishers()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter publishers_data = new SqlDataAdapter("SELECT ID,Name FROM Publishers", DataBaseTools.conn);
            publishers_data.Fill(ds, "Publishers");
            publisherCB.ItemsSource = ds.Tables["Publishers"].DefaultView;
        }

        string pdfLocation;
        private delegate void fillInfoDeligate();
        private delegate void addBookDeligate();
        private void BrowsBTN_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "PDF Files (*.pdf)|*.pdf";
            o.ShowDialog();
            pdfLocation = o.FileName;
            if (locationTB.Text != o.FileName && o.FileName != "")
            {
                progressBar.Visibility = Visibility.Visible;
                progressbarLBL.Visibility = Visibility.Visible;
                progressbarLBL.Content = "Getting book info...";
                locationTB.Text = pdfLocation;
                Thread t = new Thread(FillPDFInfo);
                t.Start();
            }
        }

        byte[] coverImage;
        PdfDocument document;
        private void FillPDFInfo()
        {
            document = PdfDocument.Load(pdfLocation);
            var image = document.Render(0, 400, 400, true);
            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Jpeg);
            coverImage = ms.ToArray();

            string[] values = pdfLocation.Split('\\');
            string title = values[values.Length - 1];

            Dispatcher.Invoke(new Action(() => 
            {
                string isbn = PDFTools.GetISBN(locationTB.Text);
                pagesCountTB.Text = PDFTools.GetPagesCount(pdfLocation) + "";
                coverPicIE.Source = LoadImage(coverImage);
                titleTB.Text = title.TrimEnd(".pdf".ToCharArray());
                textBoxISBN.Text = isbn;
                progressBar.Visibility = Visibility.Hidden;
                progressbarLBL.Content = "Done getting info.";
            }));
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

        public static Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }

        private void addCatBTN_Click(object sender, RoutedEventArgs e)
        {
            AddCategory a = new AddCategory();
            a.ShowDialog();
            fillCategories();
        }

        private void addAuthorBTN_Click(object sender, RoutedEventArgs e)
        {
            AddAuthor a = new AddAuthor();
            a.ShowDialog();
            FillAuthors();
        }

        private void addPublisherBTN_Click(object sender, RoutedEventArgs e)
        {
            AddPublisher a = new AddPublisher();
            a.ShowDialog();
            FillPublishers();
        }

        private void addBookBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                progressBar.Visibility = Visibility.Visible;
                progressbarLBL.Content = "Adding book...";
                int bookID = int.Parse(BookIDTB.Text);
                string bookTitle = titleTB.Text;
                string pdfLocation = locationTB.Text;
                int categoryID = (int)categoryCB.SelectedValue;
                string date = pubDate.DateTime.ToShortDateString();
                int pagesCount = int.Parse(pagesCountTB.Text);
                int publisherID = (int)publisherCB.SelectedValue;
                string ISBN = isbnTB.Text;
                string description = descriptionTB.Text;
                disable();
                Thread t = new Thread(() => addBookFunc(bookID, bookTitle, pdfLocation, categoryID, date, pagesCount, publisherID, ISBN, description, coverImage, document));
                t.Start();
            }
            catch (Exception ex)
            {
                progressbarLBL.Content = "Error !!....check your inputs\nor the connection";
                progressBar.Visibility = Visibility.Hidden;
            }
           
        }
        private void disable()
        {
            BookIDTB.IsReadOnly = true;
            titleTB.IsReadOnly = true;
            locationTB.IsReadOnly = true;
            categoryCB.IsEnabled = false;
            pubDate.IsEnabled = false;
            publisherCB.IsReadOnly = true;
            isbnTB.IsReadOnly = true;
            descriptionTB.IsReadOnly = true;
            addAuthorBTN.IsEnabled = false;
            addBookBTN.IsEnabled = false;
            cancelBTN.IsEnabled = false;
        }
        public void addBookFunc(int bookID,string bookTitle, string pdfLocation, int categoryID, string date, int pagesCount, int publisherID, string ISBN, string description, byte[] coverImage, PdfDocument document)
        {
            DataBaseTools.AddBook(bookTitle, pdfLocation, categoryID, date, pagesCount, publisherID, ISBN, description, coverImage, document);
            foreach (var item in AuthorsLB.Items)
            {
                string line = item.ToString();
                string[] values = line.Split(' ');
                foreach (string value in values)
                {
                    try
                    {
                        int authorID = int.Parse(value);
                        DataBaseTools.AddBookAuthor(bookID, authorID);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            Dispatcher.Invoke(new Action(() =>
            {
                progressBar.Visibility = Visibility.Hidden;
                progressbarLBL.Content = "Done.";
                System.Windows.Forms.MessageBox.Show("Book " + titleTB.Text + " Added", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }));
        }
        private void cancelBTN_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void removeBTN_Click(object sender, RoutedEventArgs e)
        {
            coverPicIE.Source = null;
            pagesCountTB.Clear();
            locationTB.Clear();
            titleTB.Clear();
            AuthorsLB.Items.Clear();
            publisherCB.SelectedItem = null;
            textBoxISBN.Clear();
            isbnTB.Clear();
            progressbarLBL.Content = "Book Removed";
        }

        private void addtoList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int authorID = (int)authorsCB.SelectedValue;
                string name = DataBaseTools.GetAuthorName(authorID);
                foreach (var item in AuthorsLB.Items)
                {
                    if (authorID + " " + name == (string)item)
                    {
                        return;
                    }
                }
                AuthorsLB.Items.Add(authorID + " " + name);
            }
            catch (Exception)
            {

            }
        }
        private void viewBooksBTN_Click(object sender, RoutedEventArgs e)
        {
            PDFViewer p = new PDFViewer(locationTB.Text);
            p.Show();
        }
        private void AuthorsLB_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            AuthorsLB.Items.RemoveAt(AuthorsLB.SelectedIndex);
        }

        private void pubDate_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           
        }

        private void pubDate_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (pubDate.DateTime > DateTime.Now)
                System.Windows.Forms.MessageBox.Show("Publish date can't be in future !","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            pubDate.DateTime = DateTime.Now;
        }
    }
}
