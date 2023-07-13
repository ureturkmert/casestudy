using HPASS.DataAccessLayer.Abstraction;
using HPASS.EfCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPASS.DataAccessLayer.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly HpassDbContext dbContext;
        public UnitOfWork(HpassDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public void Commit()
        {
            try
            {
                this.dbContext.SaveChanges();
                this.dbContext.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.RollBack();
                throw ex;
            }
        }

        public void RollBack()
        {
            this.dbContext.Database.RollbackTransaction();
        }
    }
}
