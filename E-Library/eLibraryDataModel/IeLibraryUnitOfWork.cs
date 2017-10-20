using E_Library.Common.DataModel;

namespace E_Library.eLibraryDataModel
{
    /// <summary>
    /// IeLibraryUnitOfWork extends the IUnitOfWork interface with repositories representing specific entities.
    /// </summary>
    public interface IeLibraryUnitOfWork : IUnitOfWork
    {

        /// <summary>
        /// The Author entities repository.
        /// </summary>
        IRepository<Author, int> Authors { get; }

        /// <summary>
        /// The BooksAuthor entities repository.
        /// </summary>
        IRepository<BooksAuthor, int> BooksAuthors { get; }

        /// <summary>
        /// The Book entities repository.
        /// </summary>
        IRepository<Book, int> Books { get; }

        /// <summary>
        /// The Category entities repository.
        /// </summary>
        IRepository<Category, int> Categories { get; }

        /// <summary>
        /// The Publisher entities repository.
        /// </summary>
        IRepository<Publisher, int> Publishers { get; }
    }
}
