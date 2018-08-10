using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HBKSolution.Services
{
    public class NinjectResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectResolver()
        {
            _kernel = new StandardKernel();
            _kernel.Unbind <ModelValidatorProvider>();
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            // _kernel.Bind<To, From>(); // Registering Types    
            _kernel.Bind<IDataAccessService>().To<DataAccessService>();
            _kernel.Bind<IProductCategoryService>().To<ProductCategoryService>();
            _kernel.Bind<IProductService>().To<ProductService>();
            _kernel.Bind<IDataProjectService>().To<DataProjectService>();
        }
    }
}