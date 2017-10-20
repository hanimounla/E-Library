using E_Library.Common.DataModel;
using E_Library.Common.DataModel.DesignTime;

namespace E_Library.eLibraryDataModel
{
    /// <summary>
    /// A eLibraryDesignTimeUnitOfWork instance that represents the design-time implementation of the IeLibraryUnitOfWork interface.
    /// </summary>
    public class eLibraryDesignTimeUnitOfWork : DesignTimeUnitOfWork, IeLibraryUnitOfWork
    {

        /// <summary>
        /// Initializes a new instance of the eLibraryDesignTimeUnitOfWork class.
        /// </summary>
        public eLibraryDesignTimeUnitOfWork()
        {
        }

        IRepository<Author, int> IeLibraryUnitOfWork.Authors
        {
            get { return GetRepository((Author x) => x.ID); }
        }

        IRepository<BooksAuthor, int> IeLibraryUnitOfWork.BooksAuthors
        {
            get { return GetRepository((BooksAuthor x) => x.ID); }
        }

        IRepository<Book, int> IeLibraryUnitOfWork.Books
        {
            get { return GetRepository((Book x) => x.ID); }
        }

        IRepository<Category, int> IeLibraryUnitOfWork.Categories
        {
            get { return GetRepository((Category x) => x.ID); }
        }

        IRepository<Publisher, int> IeLibraryUnitOfWork.Publishers
        {
            get { return GetRepository((Publisher x) => x.ID); }
        }
    }
}
