using System;
using System.Data;
using System.Data.SqlClient;

namespace E_Library
{
    class DataBaseTools
    {
        public static string connstr = @"Data Source=HANI\SQLSERVER2014;Initial Catalog=E-LibraryDB;Integrated Security=True";
        public static SqlConnection conn = new SqlConnection(connstr);

        public static void AddBook(string name, int category, DateTime publishdate, int publisher, string description , string location , long isbn)
        {
            conn.Open();
            string proceduer = "sp_AddBook";
            SqlCommand cmd = new SqlCommand(proceduer, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@categoryID",category);
            cmd.Parameters.AddWithValue("@publishDate",publishdate);
            cmd.Parameters.AddWithValue("@publisherID",publisher);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@location", location);
            cmd.Parameters.AddWithValue("@isbn", isbn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void AddAuthor(string name , string nationality)
        {
            conn.Open();
            string proceduer = "sp_AddAuthor";
            SqlCommand cmd = new SqlCommand(proceduer, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@nationality", nationality);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void AddCategory(string name)
        {
            conn.Open();
            string proceduer = "sp_AddCategory";
            SqlCommand cmd = new SqlCommand(proceduer, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", name);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void AddPublisher(string name , string city , string phone)
        {
            conn.Open();
            string proceduer = "sp_AddPublisher";
            SqlCommand cmd = new SqlCommand(proceduer, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@city", city);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static string GetAuthorName(int id)
        {
            string query = "select name from authors where id = " + id;
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            string name = cmd.ExecuteScalar().ToString();
            conn.Close();
            return name; 
        }

        public static void AddBookAuthor(int bookID , int authorID)
        {
            conn.Open();
            string proceduer = "sp_AddBookAuthor";
            SqlCommand cmd = new SqlCommand(proceduer, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@bookID", bookID);
            cmd.Parameters.AddWithValue("@authorID", authorID);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static int GetLastBook(string name)
        {
            string query = "select max(id) from books";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            int bookid = (int)cmd.ExecuteScalar();
            conn.Close();
            return bookid;
        }

        public static void InsertBookPage(int bookID, string text)
        {
            string proceduer = "sp_AddBookPage";
            SqlCommand cmd = new SqlCommand(proceduer, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@bookID", bookID);
            cmd.Parameters.AddWithValue("@text", text);
            cmd.ExecuteNonQuery();
        }
    }
    
}
