using DevExpress.Mvvm.POCO;
using E_Library.Common.DataModel;
using E_Library.Common.ViewModel;
using E_Library.eLibraryDataModel;

namespace E_Library.ViewModels
{
    /// <summary>
    /// Represents the BooksAuthors collection view model.
    /// </summary>
    public partial class BooksAuthorCollectionViewModel : CollectionViewModel<BooksAuthor, int, IeLibraryUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of BooksAuthorCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static BooksAuthorCollectionViewModel Create(IUnitOfWorkFactory<IeLibraryUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new BooksAuthorCollectionViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the BooksAuthorCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the BooksAuthorCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected BooksAuthorCollectionViewModel(IUnitOfWorkFactory<IeLibraryUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.BooksAuthors)
        {
        }
    }
}