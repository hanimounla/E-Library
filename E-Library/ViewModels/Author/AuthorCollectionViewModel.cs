using DevExpress.Mvvm.POCO;
using E_Library.Common.DataModel;
using E_Library.Common.ViewModel;
using E_Library.eLibraryDataModel;

namespace E_Library.ViewModels
{
    /// <summary>
    /// Represents the Authors collection view model.
    /// </summary>
    public partial class AuthorCollectionViewModel : CollectionViewModel<Author, int, IeLibraryUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of AuthorCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static AuthorCollectionViewModel Create(IUnitOfWorkFactory<IeLibraryUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new AuthorCollectionViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the AuthorCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the AuthorCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected AuthorCollectionViewModel(IUnitOfWorkFactory<IeLibraryUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Authors)
        {
        }
    }
}