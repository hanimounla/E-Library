using E_Library.Common.DataModel;
using E_Library.Common.DataModel.EntityFramework;
using System;

namespace E_Library.eLibraryDataModel
{
    /// <summary>
    /// A eLibraryUnitOfWork instance that represents the run-time implementation of the IeLibraryUnitOfWork interface.
    /// </summary>
    public class eLibraryUnitOfWork : DbUnitOfWork<eLibrary>, IeLibraryUnitOfWork
    {

        public eLibraryUnitOfWork(Func<eLibrary> contextFactory)
            : base(contextFactory)
        {
        }

        IRepository<Author, int> IeLibraryUnitOfWork.Authors
        {
            get { return GetRepository(x => x.Set<Author>(), x => x.ID); }
        }

        IRepository<BooksAuthor, int> IeLibraryUnitOfWork.BooksAuthors
        {
            get { return GetRepository(x => x.Set<BooksAuthor>(), x => x.ID); }
        }

        IRepository<Book, int> IeLibraryUnitOfWork.Books
        {
            get { return GetRepository(x => x.Set<Book>(), x => x.ID); }
        }

        IRepository<Category, int> IeLibraryUnitOfWork.Categories
        {
            get { return GetRepository(x => x.Set<Category>(), x => x.ID); }
        }

        IRepository<Publisher, int> IeLibraryUnitOfWork.Publishers
        {
            get { return GetRepository(x => x.Set<Publisher>(), x => x.ID); }
        }
    }
}
