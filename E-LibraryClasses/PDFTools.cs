using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Windows.Forms;

namespace E_Library
{
    class PDFTools
    {
        public static void InsertBookPages(int bookID, string path)
        {
            DataBaseTools.conn.Open();
            PdfReader reader = new PdfReader(path);
            string pagetext = "";
            for (int i = 1; i <= reader.NumberOfPages; i++)
            {
                pagetext = PdfTextExtractor.GetTextFromPage(reader, i);
                DataBaseTools.InsertBookPage(bookID, pagetext);
            }
            DataBaseTools.conn.Close();
            MessageBox.Show("Book " + bookID + " added with pages", "info", MessageBoxButtons.OK);

        }

        public static string GetISBN(string path)
        {
            PdfReader reader = new PdfReader(path);
            string isbn = "";
            string pagetext = "";
            for (int i = 1; i <= 10; i++)
            {
                pagetext = PdfTextExtractor.GetTextFromPage(reader, i);
                string[] words = pagetext.Split(' ');
                int wordsInPage = words.Length;
                int currentWordIndex = 0;
                foreach (string word in words)
                {
                    if(word.Contains("ISBN"))
                    {
                        isbn = words[++currentWordIndex];
                        return isbn;
                    }
                    currentWordIndex++;
                }
            }
            return "ISBN coulden't be found";
        }
        public static byte[] GetCoverPic(string path)
        {
            byte[] coverPic = new byte[5000];
            PdfReader m = new PdfReader(path);
            
            return coverPic;

        }
    }
}
