using Ninject.Modules;
using Ninject.Web.Common;
using ProductManagement.Models;

namespace ProductManagement.Initialisation
{
    /// <summary>
    /// Services Module class to perform initializations
    /// </summary>
    public class ServicesModule: NinjectModule
    {
        /// <summary>
        /// Overloaded Ninject Module used for DI mapping Types 
        /// </summary>
        public override void Load()
        {
            Bind<IProductsRepository>().To<ProductsRepository>().InRequestScope();
            Bind<IProductOptionsRepository>().To<ProductOptionsRepository>().InRequestScope();
        }
    }
}