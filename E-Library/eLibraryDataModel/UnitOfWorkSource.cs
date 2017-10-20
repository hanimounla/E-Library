using DevExpress.Mvvm;
using E_Library.Common.DataModel;
using E_Library.Common.DataModel.DesignTime;
using E_Library.Common.DataModel.EntityFramework;

namespace E_Library.eLibraryDataModel
{
    /// <summary>
    /// Provides methods to obtain the relevant IUnitOfWorkFactory.
    /// </summary>
    public static class UnitOfWorkSource
    {
        /// <summary>
        /// Returns the IUnitOfWorkFactory implementation based on the current mode (run-time or design-time).
        /// </summary>
        public static IUnitOfWorkFactory<IeLibraryUnitOfWork> GetUnitOfWorkFactory()
        {
            return GetUnitOfWorkFactory(ViewModelBase.IsInDesignMode);
        }

        /// <summary>
        /// Returns the IUnitOfWorkFactory implementation based on the given mode (run-time or design-time).
        /// </summary>
        /// <param name="isInDesignTime">Used to determine which implementation of IUnitOfWorkFactory should be returned.</param>
        public static IUnitOfWorkFactory<IeLibraryUnitOfWork> GetUnitOfWorkFactory(bool isInDesignTime)
        {
            if (isInDesignTime)
                return new DesignTimeUnitOfWorkFactory<IeLibraryUnitOfWork>(() => new eLibraryDesignTimeUnitOfWork());
            return new DbUnitOfWorkFactory<IeLibraryUnitOfWork>(() => new eLibraryUnitOfWork(() => new eLibrary()));
        }
    }
}