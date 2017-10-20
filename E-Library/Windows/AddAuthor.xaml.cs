using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace E_Library
{
    /// <summary>
    /// Interaction logic for AddAuthor.xaml
    /// </summary>
    public partial class AddAuthor : DevExpress.Xpf.Core.DXWindow
    {
        public AddAuthor()
        {
            InitializeComponent();
        }

        public void addBTN_Click(object sender, RoutedEventArgs e)
        {
            byte[] image = null;
            if (!string.IsNullOrWhiteSpace(nameTB.Text))
            {
                if (PicIE.Source != null)
                {
                    string path = (PicIE.Source as BitmapImage).UriSource.OriginalString;
                    image = Getimage(path);
                }
                else
                {
                    Bitmap bmap = Properties.Resources.personIcon;
                    MemoryStream stream = new MemoryStream();
                    bmap.Save(stream, bmap.RawFormat);
                    image = stream.ToArray();

                }
                DataBaseTools.AddAuthor(nameTB.Text,nationalityTB.Text,bioTB.Text,image);
                System.Windows.Forms.MessageBox.Show("Author added");
                Close();
            }
            else
                System.Windows.Forms.MessageBox.Show("Enter Author Name");
        }

        public byte[] Getimage(string path)
        {
            FileStream stream = new FileStream(
            path, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);

            byte[] photo = reader.ReadBytes((int)stream.Length);

            reader.Close();
            stream.Close();

            return photo;
        }

        private void cancelBTN_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void browsBTN_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            //o.Filter = "Images files (*.jpg)|*.jpg";
            o.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            o.ShowDialog();
            if (!string.IsNullOrEmpty(o.FileName))
            {
                PicIE.Source = new BitmapImage(new System.Uri(o.FileName));

            }
            //if (o.FileName != ((BitmapFrame)PicIE.Source).Decoder.ToString())
            //{
            //    PicIE.Source = new BitmapImage(new System.Uri(o.FileName));

            //}
        }

        private void clearBTN_Click(object sender, RoutedEventArgs e)
        {
            PicIE.Source = null;
        }

        private void searchOnlineBTN_Click(object sender, RoutedEventArgs e)
        {
            string searchRequest = nameTB.Text;
            //searchRequest = new System.Text.RegularExpressions.Regex("(?<=for ?).+$").Match(searchRequest).Value;
            Process.Start("http://www.google.com/search?q=" + searchRequest + " Images");
        }
    }
}
