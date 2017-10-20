namespace E_Library
{
    using System.Data.Entity;

    public partial class eLibrary : DbContext
    {
        public eLibrary()
            : base("name=eLibrary")
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BooksAuthor> BooksAuthors { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(e => e.BooksAuthors)
                .WithRequired(e => e.Author)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.ISBN)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.BooksAuthors)
                .WithRequired(e => e.Book)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Publisher>()
                .Property(e => e.Contact)
                .IsUnicode(false);
        }
    }
}
