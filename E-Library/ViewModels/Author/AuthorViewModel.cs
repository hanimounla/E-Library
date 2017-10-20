using DevExpress.Mvvm.POCO;
using E_Library.Common.DataModel;
using E_Library.Common.ViewModel;
using E_Library.eLibraryDataModel;

namespace E_Library.ViewModels
{
    /// <summary>
    /// Represents the single Author object view model.
    /// </summary>
    public partial class AuthorViewModel : SingleObjectViewModel<Author, int, IeLibraryUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of AuthorViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static AuthorViewModel Create(IUnitOfWorkFactory<IeLibraryUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new AuthorViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the AuthorViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the AuthorViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected AuthorViewModel(IUnitOfWorkFactory<IeLibraryUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Authors, x => x.Name)
        {
        }

        /// <summary>
        /// The view model for the AuthorBooksAuthors detail collection.
        /// </summary>
        public CollectionViewModel<BooksAuthor, int, IeLibraryUnitOfWork> AuthorBooksAuthorsDetails
        {
            get { return GetDetailsCollectionViewModel((AuthorViewModel x) => x.AuthorBooksAuthorsDetails, x => x.BooksAuthors, x => x.AuthorID, (x, key) => x.AuthorID = key); }
        }
    }
}
