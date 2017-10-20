using DevExpress.Xpf.DocumentViewer;
using System.Threading;
using System.Windows;

namespace E_Library
{
    /// <summary>
    /// Interaction logic for PDFViewer.xaml
    /// </summary>
    public partial class PDFViewer : DevExpress.Xpf.Core.DXWindow
    {
        public PDFViewer()
        {
            InitializeComponent();
        }
        public PDFViewer(string path)
        {
            InitializeComponent();
            pdfControl.OpenDocument(path);
        }

        public PDFViewer(string path , int pageID , string keyWord)
        {
            InitializeComponent();
            TextSearchParameter tsp = new TextSearchParameter();
            tsp.Text = keyWord;
            tsp.CurrentPage = pageID;
            
            pdfControl.OpenDocument(path);

            Application.Current.Dispatcher.BeginInvoke(
             System.Windows.Threading.DispatcherPriority.Background,
                new System.Action(() => pdfControl.CurrentPageNumber = pageID));

            //Thread.Sleep(1000);

            Application.Current.Dispatcher.BeginInvoke(
             System.Windows.Threading.DispatcherPriority.Background,
                new System.Action(() => pdfControl.FindText(tsp)));
        }
    }
}

