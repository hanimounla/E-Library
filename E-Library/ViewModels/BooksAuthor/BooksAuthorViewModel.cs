using DevExpress.Mvvm.POCO;
using E_Library.Common.DataModel;
using E_Library.Common.ViewModel;
using E_Library.eLibraryDataModel;

namespace E_Library.ViewModels
{
    /// <summary>
    /// Represents the single BooksAuthor object view model.
    /// </summary>
    public partial class BooksAuthorViewModel : SingleObjectViewModel<BooksAuthor, int, IeLibraryUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of BooksAuthorViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static BooksAuthorViewModel Create(IUnitOfWorkFactory<IeLibraryUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new BooksAuthorViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the BooksAuthorViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the BooksAuthorViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected BooksAuthorViewModel(IUnitOfWorkFactory<IeLibraryUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.BooksAuthors, x => x.ID)
        {
        }

        /// <summary>
		/// The view model that contains a look-up collection of Authors for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<Author> LookUpAuthors
        {
            get { return GetLookUpEntitiesViewModel((BooksAuthorViewModel x) => x.LookUpAuthors, x => x.Authors); }
        }

        /// <summary>
		/// The view model that contains a look-up collection of Books for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<Book> LookUpBooks
        {
            get { return GetLookUpEntitiesViewModel((BooksAuthorViewModel x) => x.LookUpBooks, x => x.Books); }
        }
    }
}
