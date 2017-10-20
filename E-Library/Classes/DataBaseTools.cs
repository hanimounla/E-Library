using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace E_Library
{
    class DataBaseTools
    {
        public static string connstr = @"data source=Hani\SQLSERVER2014;initial catalog=E-LibraryDB;User id = hani mounla; password = hanihani;MultipleActiveResultSets=True;App=EntityFramework providerName=System.Data.SqlClient";
        public static SqlConnection conn = new SqlConnection(connstr);

        public static void open()
        {
            try
            {
                conn.Open();
            }
            catch (System.Exception)
            {

            }
        }
        public static void close()
        {
            try
            {
                conn.Close();
            }
            catch (System.Exception)
            {

            }
        }

        internal static void AddBook(string bookTitle, string pdfLocation, int categoryID, 
            string publishDate,int pagesCount, int publisherID, string ISBN, string description, byte[] coverImage,object pdfDocument)
        {
            open();
            string proceduer = "sp_AddBook";
            SqlCommand cmd = new SqlCommand(proceduer, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@title", bookTitle);
            cmd.Parameters.AddWithValue("@categoryID", categoryID);
            cmd.Parameters.AddWithValue("@publishDate", publishDate);
            cmd.Parameters.AddWithValue("@publisherID", publisherID);
            cmd.Parameters.AddWithValue("@pagesCount", pagesCount);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@location", pdfLocation);
            cmd.Parameters.AddWithValue("@isbn", ISBN);
            cmd.Parameters.AddWithValue("@coverPic", coverImage);
            cmd.ExecuteNonQuery();
            int bookID = GetLastBook("");

            PDFTools.InsertBookPages(bookID, pdfLocation, pagesCount);
            close();

        }

        internal static void UpdateUserPassword(int userID , string NewPass)
        {
            string query = "Update Users set Password = '" + NewPass + "' where ID = " + userID;
            SqlCommand cmd = new SqlCommand(query, conn);
            open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void InsertBookPage(int bookID, string text, string pictureText)
        {
            string proceduer = "sp_AddBookPage";
            SqlCommand cmd = new SqlCommand(proceduer, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@bookID", bookID);
            cmd.Parameters.AddWithValue("@text", text);
            cmd.Parameters.AddWithValue("@pictureText", pictureText);
            cmd.ExecuteNonQuery();
        }

        internal static void DeleteBook(int bookID)
        {
            open();
            string proceduer = "sp_DeleteBook";
            SqlCommand cmd = new SqlCommand(proceduer, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@bookID", bookID);
            cmd.ExecuteNonQuery();
            close();
        }
        internal static byte[] GetPDFFile(int bookId)
        {
            open();
            string location = "";
            string query = "Select location from Books where ID = " + bookId;
            SqlCommand cmd = new SqlCommand(query, conn);
            location = cmd.ExecuteScalar().ToString();
            byte[] PDFFile = File.ReadAllBytes(location);
            InsetIntoTemp(bookId, PDFFile);
            close();
            return PDFFile;
        }

        internal static void InsetIntoTemp(int bookID ,byte [] PDFFile)
        {
            string proceduer = "sp_InsertTempBook";
            SqlCommand cmd = new SqlCommand(proceduer, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@bookID", bookID);
            cmd.Parameters.AddWithValue("@pdfFile", PDFFile);
            cmd.ExecuteNonQuery();
        }

        public static void AddBookAuthor(int bookID, int authorID)
        {
            open();
            string proceduer = "sp_AddBookAuthor";
            SqlCommand cmd = new SqlCommand(proceduer, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@bookID", bookID);
            cmd.Parameters.AddWithValue("@authorID", authorID);
            cmd.ExecuteNonQuery();
            close();
        }
        public static void AddAuthor(string name , string nationality,string bio, byte [] image)
        {
            open();
            string proceduer = "sp_AddAuthor";
            SqlCommand cmd = new SqlCommand(proceduer, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@nationality", nationality);
            cmd.Parameters.AddWithValue("@authorPic", image);
            cmd.Parameters.AddWithValue("@bio", bio);
            cmd.ExecuteNonQuery();
            close();
        }

        public static void AddCategory(string name , string description)
        {
            open();
            string proceduer = "sp_AddCategory";
            SqlCommand cmd = new SqlCommand(proceduer, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.ExecuteNonQuery();
            close();
        }

        public static void AddPublisher(string name , string address , string contact , byte [] image , string details)
        {
            open();
            string proceduer = "sp_AddPublisher";
            SqlCommand cmd = new SqlCommand(proceduer, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@contact", contact);
            cmd.Parameters.AddWithValue("@publisherPic", image);
            cmd.Parameters.AddWithValue("@details", details);
            cmd.ExecuteNonQuery();
            close();
        }

        public static string GetAuthorName(int id)
        {
            string query = "select name from authors where id = " + id;
            SqlCommand cmd = new SqlCommand(query, conn);
            open();
            string name = cmd.ExecuteScalar().ToString();
            close();
            return name; 
        }
        public static int GetLastBook(string name)
        {
            string query = "select max(id) from books";
            SqlCommand cmd = new SqlCommand(query, conn);
            int bookid = 1;
            try
            {
                 bookid = (int)cmd.ExecuteScalar();
            }
            catch (System.Exception)
            {
            }
            return bookid;
        }

        public static byte [] GetCoverPic(int bookID)
        {
            byte[] coverPic = null;
            string query = "select CoverPicture from books where ID = " + bookID;
            SqlCommand cmd = new SqlCommand(query, conn);
            open();
            coverPic = (byte [])cmd.ExecuteScalar();
            close();
            return coverPic;
        }

        internal static string GetPdfLocation(string title)
        {
            string location = "";
            string query = "select location from books where Title = N'" + title + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            open();
            var row = cmd.ExecuteReader();
            row.Read();
            location = row[0].ToString();
            close();
            return location;
        }
    }
}
