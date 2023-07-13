using HPASS.Crosscutting.Abstraction;
using HPASS.DataAccessLayer.Abstraction;
using HPASS.EfCore.Context;
using HPASS.Entity.Base.Abstraction;
using HPASS.Response.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

//https://medium.com/@semihelitas/generic-repository-pattern-asp-net-core-e2d275ba0e
//https://learn.microsoft.com/en-us/ef/core/
//https://learn.microsoft.com/en-us/ef/core/modeling/

namespace HPASS.DataAccessLayer.Implementation
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private bool disposed = false;
        private readonly HpassDbContext dbContext;
        private readonly IUserSessionContext userSessionContext;

        public Repository(HpassDbContext dbContext, IUserSessionContext userSessionContext)
        {
            this.dbContext = dbContext;
            this.userSessionContext = userSessionContext;
        }

        public bool EnsureDatabaseDesignCompatibility()
        {

            bool deletionCheck = this.dbContext.Database.EnsureDeleted();
            bool creationCheck = this.dbContext.Database.EnsureCreated();

            return deletionCheck || creationCheck;
        }

        public void Attach(T entity)
        {
            this.BeginTransactionIfNeeded();

            entity.Id = Guid.NewGuid();
            entity.CreationDate = DateTime.UtcNow;
            if (userSessionContext is not null && userSessionContext.UserId != Guid.Empty)
            {
                entity.CreatedBy = userSessionContext.UserId;
            }

            this.dbContext.Set<T>().Add(entity);

        }

        public void Update(T entity)
        {

            this.BeginTransactionIfNeeded();
            entity.ModificationDate = DateTime.UtcNow;
            if (userSessionContext is not null && userSessionContext.UserId != Guid.Empty)
            {
                entity.ModifiedBy = userSessionContext.UserId;
            }

            this.dbContext.Entry(entity).State = EntityState.Modified;
        }

        public PagingResponse<T> Paging(int pageNumber, int pageSize, Expression<Func<T, bool>> expressionFunction = null)
        {
            PagingResponse<T> returningResult = new PagingResponse<T>();

            if (expressionFunction is not null)
            {
                returningResult.NumberOfTotalEntities = this.dbContext.Set<T>().Count(expressionFunction);
            }
            else
            {
                returningResult.NumberOfTotalEntities = this.dbContext.Set<T>().Count();
            }
            if (expressionFunction is not null && returningResult.NumberOfTotalEntities > 0)
            {
                returningResult.Entities = this.dbContext.Set<T>().Where(expressionFunction).OrderBy(x => x.CreationDate).Skip(pageNumber * pageSize).Take(pageSize).AsNoTracking().ToList();
            }
            else
            {
                returningResult.Entities = this.dbContext.Set<T>().OrderBy(x => x.CreationDate).Skip(pageNumber * pageSize).Take(pageSize).AsNoTracking().ToList();
            }

            return returningResult;
        }

        public int Count()
        {
            return this.dbContext.Set<T>().Count();
        }
        public int Count(Expression<Func<T, bool>> expressionFunction)
        {
            return this.dbContext.Set<T>().Count(expressionFunction);
        }
        public T FirstOrDefault(Expression<Func<T, bool>> expressionFunction, bool asTracked = false)
        {
            if (asTracked)
            {
                return this.dbContext.Set<T>().AsTracking().FirstOrDefault(expressionFunction);
            }
            else
            {
                return this.dbContext.Set<T>().AsNoTracking().FirstOrDefault(expressionFunction);
            }


        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expressionFunction, bool asTracked = false)
        {
            if (asTracked)
            {
                return this.dbContext.Set<T>().AsTracking().Where(expressionFunction).ToList();
            }
            else
            {
                return this.dbContext.Set<T>().AsNoTracking().Where(expressionFunction).ToList();
            }
        }

        public IEnumerable<T> GetAll(bool asTracked = false)
        {
            if (asTracked)
            {
                return this.dbContext.Set<T>().AsTracking().ToList();
            }
            else
            {
                return this.dbContext.Set<T>().AsNoTracking().ToList();
            }

        }

        private void BeginTransactionIfNeeded()
        {

            if (this.dbContext.Database.CurrentTransaction is null)
            {
                this.dbContext.Database.BeginTransaction();
            }
        }
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}