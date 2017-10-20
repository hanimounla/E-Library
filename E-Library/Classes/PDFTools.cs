using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Tesseract;

namespace E_Library
{
    class PDFTools
    {
        public  static string GetISBN(string path)
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
                    if (word.Contains("ISBN"))
                    {
                        isbn = words[++currentWordIndex];
                        return isbn;
                    }
                    currentWordIndex++;
                }
            }
            return "ISBN couldn't be found";
        }
        public static int GetPagesCount(string path)
        {
            PdfReader reader = new PdfReader(path);
            return reader.NumberOfPages;
        }
        public static void InsertBookPages(int bookID, string path,int pagesCount)
        {
            string pagetext = "";
            string picturesText = "";
            PdfReader reader = new PdfReader(path);
            RandomAccessFileOrArray raf = new RandomAccessFileOrArray(path);
            for (int pageID = 1; pageID <= pagesCount; pageID++)
            {
                pagetext = ConvertArabic(PdfTextExtractor.GetTextFromPage(reader, pageID));
                picturesText = GetPictureText(pageID, reader, raf);
                DataBaseTools.InsertBookPage(bookID, pagetext, picturesText);
            }
            raf.Close();
            reader.Close();
        }
        #region tera
        private static string ConvertArabic(string source)
        {
            string arabicWord = string.Empty;
            StringBuilder sbDestination = new StringBuilder();
            foreach (var ch in source)
            {
                if (IsArabic(ch) || ch == ' ')
                    arabicWord += ch;
                else
                {
                    if (arabicWord != string.Empty)
                        sbDestination.Append(Reverse(arabicWord));
                    string a = sbDestination.ToString();
                    sbDestination.Append(ch);
                    arabicWord = string.Empty;
                }
            }

            // if the last word was Arabic    
            if (arabicWord != string.Empty)
                sbDestination.Append(Reverse(arabicWord));


            return sbDestination.ToString();
        }


        private static  bool IsArabic(char character)
        {
            if (character >= 0x600 && character <= 0x6ff)
                return true;

            if (character >= 0x750 && character <= 0x77f)
                return true;

            if (character >= 0xfb50 && character <= 0xfc3f)
                return true;

            if (character >= 0xfe70 && character <= 0xfefc)
                return true;

            return false;
        }
        // Reverse the characters of string
        private static string Reverse(string source)
        {
            return new string(source.ToCharArray().Reverse().ToArray());
        }

        #endregion
        private static string GetPictureText(int pageID, PdfReader reader, RandomAccessFileOrArray raf )//look for pictures in pdf page
        {
            PdfDictionary pg = reader.GetPageN(pageID);
            // recursively search pages, forms and groups for images.
            PdfObject obj = FindImageInPDFDictionary(pg);
            if (obj != null)
            {
                int XrefIndex = Convert.ToInt32(((PRIndirectReference)obj).Number.ToString(System.Globalization.CultureInfo.InvariantCulture));
                PdfObject pdfObj = reader.GetPdfObject(XrefIndex);
                PdfStream pdfStrem = (PdfStream)pdfObj;
                byte[] bytes = PdfReader.GetStreamBytesRaw((PRStream)pdfStrem);
                if (bytes != null)
                {
                    try
                    {
                        TesseractEngine engine = new TesseractEngine(Directory.GetCurrentDirectory() + "\\tessdata\\", "Eng", EngineMode.Default);
                        MemoryStream memStream = new MemoryStream(bytes);
                        Image img = Image.FromStream(memStream);
                        byte[] tiffBytes = imageToByteArray(img);
                        return TesseractOnPage(tiffBytes, engine);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            return "No Picture";
        }
        private static PdfObject FindImageInPDFDictionary(PdfDictionary pg)
        {
            PdfDictionary res =
                (PdfDictionary)PdfReader.GetPdfObject(pg.Get(PdfName.RESOURCES));


            PdfDictionary xobj =
              (PdfDictionary)PdfReader.GetPdfObject(res.Get(PdfName.XOBJECT));
            if (xobj != null)
            {
                foreach (PdfName name in xobj.Keys)
                {
                    PdfObject obj = xobj.Get(name);
                    if (obj.IsIndirect())
                    {
                        PdfDictionary tg = (PdfDictionary)PdfReader.GetPdfObject(obj);

                        PdfName type =
                          (PdfName)PdfReader.GetPdfObject(tg.Get(PdfName.SUBTYPE));

                        //image at the root of the pdf
                        if (PdfName.IMAGE.Equals(type))
                        {
                            return obj;
                        }// image inside a form
                        else if (PdfName.FORM.Equals(type))
                        {
                            return FindImageInPDFDictionary(tg);
                        } //image inside a group
                        else if (PdfName.GROUP.Equals(type))
                        {
                            return FindImageInPDFDictionary(tg);
                        }

                    }
                }
            }
            return null;
        }
        public static byte[] imageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Tiff);
            return ms.ToArray();
        }

        private static string TesseractOnPage(byte [] bytes , TesseractEngine engine) // extract text from pictures using Tesseract
        {
            string pictureText = "";
            float meanConfedence;
            try
            {
                var img = Pix.LoadTiffFromMemory(bytes);
                var page = engine.Process(img);
                //var text = page.GetText();
                meanConfedence = page.GetMeanConfidence();
                if (meanConfedence < 0.7)
                    return "low Confidence picture";

                var iter = page.GetIterator();
                iter.Begin();
                do
                {
                    do
                    {
                        do
                        {
                            do
                            {
                                pictureText += iter.GetText(PageIteratorLevel.Word);
                                pictureText += " ";

                                if (iter.IsAtFinalOf(PageIteratorLevel.TextLine, PageIteratorLevel.Word))
                                {
                                    pictureText += "\n";
                                }
                            } while (iter.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));

                            if (iter.IsAtFinalOf(PageIteratorLevel.Para, PageIteratorLevel.TextLine))
                            {
                                pictureText += "\n";
                            }
                        } while (iter.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine));
                    } while (iter.Next(PageIteratorLevel.Block, PageIteratorLevel.Para));
                } while (iter.Next(PageIteratorLevel.Block));
                if (!string.IsNullOrWhiteSpace(pictureText))
                    return pictureText;
                else
                    return "No Picture";
            }
            catch (Exception )
            {
                //System.Windows.Forms.MessageBox.Show(d.Message);
                //Trace.TraceError(d.ToString());
                //richEditControl1.Text += "Unexpected Error: " + d.Message;
                //richEditControl1.Text += "Details: \n";
                //richEditControl1.Text += d.ToString();
                return "Error getting text from Picture";
            }
        }

        #region delete
        public static void ExtractImagesFromPDF(string sourcePdf, string outputPath)
        {
            // NOTE:  This will only get the first image it finds per page.
            PdfReader pdf = new PdfReader(sourcePdf);
            RandomAccessFileOrArray raf = new RandomAccessFileOrArray(sourcePdf);
            //try
            //{
            for (int pageNumber = 1; pageNumber <= pdf.NumberOfPages; pageNumber++)
            {
                PdfDictionary pg = pdf.GetPageN(pageNumber);

                // recursively search pages, forms and groups for images.
                PdfObject obj = FindImageInPDFDictionary(pg);
                if (obj != null)
                {
                    int XrefIndex = Convert.ToInt32(((PRIndirectReference)obj).Number.ToString(System.Globalization.CultureInfo.InvariantCulture));
                    PdfObject pdfObj = pdf.GetPdfObject(XrefIndex);
                    PdfStream pdfStrem = (PdfStream)pdfObj;
                    byte[] bytes = PdfReader.GetStreamBytesRaw((PRStream)pdfStrem);
                    if ((bytes != null))
                    {
                        using (MemoryStream memStream = new MemoryStream(bytes))
                        {
                            try
                            {
                                memStream.Position = 0;
                                Image img = Image.FromStream(memStream);
                                // must save the file while stream is open.
                                if (!Directory.Exists(outputPath))
                                    Directory.CreateDirectory(outputPath);

                                string path = System.IO.Path.Combine(outputPath, string.Format(@"{0}.jpg", pageNumber));
                                // EncoderParameters parms = new EncoderParameters(1);
                                // parms.Param[0] = new EncoderParameter(Encoder.Compression, 0);
                                // ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                                img.Save(path);
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }
                }
            }
            //}
            //catch
            //{

            //}
            //finally
            //{
            //    pdf.Close();
            //    raf.Close();
            //}


        } 
        #endregion
    }
    //public class TiffImage
    //{
    //    private string myPath;
    //    private Guid myGuid;
    //    private FrameDimension myDimension;
    //    public ArrayList myImages = new ArrayList();
    //    private int myPageCount;
    //    private Bitmap myBMP;

    //    public TiffImage(string path)
    //    {
    //        MemoryStream ms;
    //        Image myImage;

    //        myPath = path;
    //        FileStream fs = new FileStream(myPath, FileMode.Open);
    //        myImage = Image.FromStream(fs);
    //        myGuid = myImage.FrameDimensionsList[0];
    //        myDimension = new FrameDimension(myGuid);
    //        myPageCount = myImage.GetFrameCount(myDimension);
    //        for (int i = 0; i < myPageCount; i++)
    //        {
    //            ms = new MemoryStream();
    //            myImage.SelectActiveFrame(myDimension, i);
    //            myImage.Save(ms, ImageFormat.Bmp);
    //            myBMP = new Bitmap(ms);
    //            myImages.Add(myBMP);
    //            ms.Close();
    //        }
    //        fs.Close();
    //    }
    //}
}
