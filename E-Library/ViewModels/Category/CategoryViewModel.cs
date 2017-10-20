using DevExpress.Mvvm.POCO;
using E_Library.Common.DataModel;
using E_Library.Common.ViewModel;
using E_Library.eLibraryDataModel;

namespace E_Library.ViewModels
{
    /// <summary>
    /// Represents the single Category object view model.
    /// </summary>
    public partial class CategoryViewModel : SingleObjectViewModel<Category, int, IeLibraryUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of CategoryViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static CategoryViewModel Create(IUnitOfWorkFactory<IeLibraryUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new CategoryViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the CategoryViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the CategoryViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected CategoryViewModel(IUnitOfWorkFactory<IeLibraryUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Categories, x => x.Name)
        {
        }

        /// <summary>
        /// The view model for the CategoryBooks detail collection.
        /// </summary>
        public CollectionViewModel<Book, int, IeLibraryUnitOfWork> CategoryBooksDetails
        {
            get { return GetDetailsCollectionViewModel((CategoryViewModel x) => x.CategoryBooksDetails, x => x.Books, x => x.CategoryID, (x, key) => x.CategoryID = key); }
        }
    }
}
