using DevExpress.Mvvm.POCO;
using E_Library.Common.DataModel;
using E_Library.Common.ViewModel;
using E_Library.eLibraryDataModel;

namespace E_Library.ViewModels
{
    /// <summary>
    /// Represents the Publishers collection view model.
    /// </summary>
    public partial class PublisherCollectionViewModel : CollectionViewModel<Publisher, int, IeLibraryUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of PublisherCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static PublisherCollectionViewModel Create(IUnitOfWorkFactory<IeLibraryUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new PublisherCollectionViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the PublisherCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the PublisherCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected PublisherCollectionViewModel(IUnitOfWorkFactory<IeLibraryUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Publishers)
        {
        }
    }
}