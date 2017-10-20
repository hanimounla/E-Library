using DevExpress.Mvvm.POCO;
using E_Library.Common.DataModel;
using E_Library.Common.ViewModel;
using E_Library.eLibraryDataModel;

namespace E_Library.ViewModels
{
    /// <summary>
    /// Represents the single Publisher object view model.
    /// </summary>
    public partial class PublisherViewModel : SingleObjectViewModel<Publisher, int, IeLibraryUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of PublisherViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static PublisherViewModel Create(IUnitOfWorkFactory<IeLibraryUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new PublisherViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the PublisherViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the PublisherViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected PublisherViewModel(IUnitOfWorkFactory<IeLibraryUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Publishers, x => x.Name)
        {
        }

        /// <summary>
        /// The view model for the PublisherBooks detail collection.
        /// </summary>
        public CollectionViewModel<Book, int, IeLibraryUnitOfWork> PublisherBooksDetails
        {
            get { return GetDetailsCollectionViewModel((PublisherViewModel x) => x.PublisherBooksDetails, x => x.Books, x => x.PublisherID, (x, key) => x.PublisherID = key); }
        }
    }
}
