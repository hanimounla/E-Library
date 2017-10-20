using DevExpress.Mvvm.POCO;
using E_Library.Common.DataModel;
using E_Library.Common.ViewModel;
using E_Library.eLibraryDataModel;

namespace E_Library.ViewModels
{
    /// <summary>
    /// Represents the single Book object view model.
    /// </summary>
    public partial class BookViewModel : SingleObjectViewModel<Book, int, IeLibraryUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of BookViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static BookViewModel Create(IUnitOfWorkFactory<IeLibraryUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new BookViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the BookViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the BookViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected BookViewModel(IUnitOfWorkFactory<IeLibraryUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Books, x => x.Title)
        {
        }

        /// <summary>
		/// The view model that contains a look-up collection of Categories for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<Category> LookUpCategories
        {
            get { return GetLookUpEntitiesViewModel((BookViewModel x) => x.LookUpCategories, x => x.Categories); }
        }

        /// <summary>
		/// The view model that contains a look-up collection of Publishers for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<Publisher> LookUpPublishers
        {
            get { return GetLookUpEntitiesViewModel((BookViewModel x) => x.LookUpPublishers, x => x.Publishers); }
        }

        /// <summary>
        /// The view model for the BookBooksAuthors detail collection.
        /// </summary>
        public CollectionViewModel<BooksAuthor, int, IeLibraryUnitOfWork> BookBooksAuthorsDetails
        {
            get { return GetDetailsCollectionViewModel((BookViewModel x) => x.BookBooksAuthorsDetails, x => x.BooksAuthors, x => x.BookID, (x, key) => x.BookID = key); }
        }
    }
}
