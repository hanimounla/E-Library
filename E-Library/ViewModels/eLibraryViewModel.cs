using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using E_Library.Common.ViewModel;
using E_Library.eLibraryDataModel;
using System;

namespace E_Library.ViewModels
{
    /// <summary>
    /// Represents the root POCO view model for the eLibrary data model.
    /// </summary>
    public partial class eLibraryViewModel : DocumentsViewModel<eLibraryModuleDescription, IeLibraryUnitOfWork>
    {

        const string TablesGroup = "Tables";

        const string ViewsGroup = "Views";
        INavigationService NavigationService { get { return this.GetService<INavigationService>(); } }

        /// <summary>
        /// Creates a new instance of eLibraryViewModel as a POCO view model.
        /// </summary>
        public static eLibraryViewModel Create()
        {
            return ViewModelSource.Create(() => new eLibraryViewModel());
        }

        /// <summary>
        /// Initializes a new instance of the eLibraryViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the eLibraryViewModel type without the POCO proxy factory.
        /// </summary>
        protected eLibraryViewModel()
            : base(UnitOfWorkSource.GetUnitOfWorkFactory())
        {
        }

        protected override eLibraryModuleDescription[] CreateModules()
        {
            return new eLibraryModuleDescription[] {
                new eLibraryModuleDescription("Books", "BookCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.Books)),
                new eLibraryModuleDescription("Categories", "CategoryCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.Categories)),
                new eLibraryModuleDescription("Authors", "AuthorCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.Authors)),
                new eLibraryModuleDescription("Publishers", "PublisherCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.Publishers)),
            };
        }



        protected override void OnActiveModuleChanged(eLibraryModuleDescription oldModule)
        {
            if (ActiveModule != null && NavigationService != null)
            {
                NavigationService.ClearNavigationHistory();
            }
            base.OnActiveModuleChanged(oldModule);
        }
    }

    public partial class eLibraryModuleDescription : ModuleDescription<eLibraryModuleDescription>
    {
        public eLibraryModuleDescription(string title, string documentType, string group, Func<eLibraryModuleDescription, object> peekCollectionViewModelFactory = null)
            : base(title, documentType, group, peekCollectionViewModelFactory)
        {
        }
    }
}