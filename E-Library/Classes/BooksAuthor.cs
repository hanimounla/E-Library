namespace E_Library
{
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class BooksAuthor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int BookID { get; set; }

        public int AuthorID { get; set; }

        public virtual Author Author { get; set; }

        public virtual Book Book { get; set; }
    }
}
