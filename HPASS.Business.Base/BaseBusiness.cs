using Autofac;
using HPASS.AutoMapping.Abstraction;
using HPASS.DataAccessLayer.Abstraction;

namespace HPASS.Business.Base
{
    public class BaseBusiness
    {
        protected readonly ILifetimeScope container;
        private IUnitOfWork unitOfWork;
        private IMappingManager mappingManager;

        protected IUnitOfWork UnitOfWork
        {
            get
            {
                if (this.unitOfWork == null)
                {
                    this.ResolveUnitOfWorkForBaseBusiness();
                }
                return this.unitOfWork;
            }
        }

        protected IMappingManager MappingManager
        {
            get
            {
                if (this.mappingManager == null)
                {
                    this.ResolveMappingManagerForBaseBusiness();
                }
                return this.mappingManager;
            }
        }

        private void ResolveMappingManagerForBaseBusiness()
        {
            this.mappingManager = this.container.Resolve<IMappingManager>();
        }

        private void ResolveUnitOfWorkForBaseBusiness()
        {
            this.unitOfWork = this.container.Resolve<IUnitOfWork>();
        }

        public BaseBusiness(ILifetimeScope container)
        {
            this.container = container;


        }
    }

}