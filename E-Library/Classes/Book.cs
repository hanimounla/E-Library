namespace E_Library
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            BooksAuthors = new HashSet<BooksAuthor>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        public int? CategoryID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PublishDate { get; set; }

        public int? PublisherID { get; set; }

        public int PagesCount { get; set; }

        public string Description { get; set; }

        [StringLength(150)]
        public string Location { get; set; }

        [StringLength(50)]
        public string ISBN { get; set; }

        [Column(TypeName = "image")]
        public byte[] CoverPicture { get; set; }

        public virtual Category Category { get; set; }

        public virtual Publisher Publisher { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BooksAuthor> BooksAuthors { get; set; }
    }
}
