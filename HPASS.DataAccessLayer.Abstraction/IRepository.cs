using HPASS.Entity.Base.Abstraction;
using HPASS.Response.Common;
using System.Linq.Expressions;

namespace HPASS.DataAccessLayer.Abstraction
{
    public interface IRepository<T> : IDisposable where T : class, IEntity
    {
        IEnumerable<T> GetAll(bool asTracked = false);
        bool EnsureDatabaseDesignCompatibility();
        void Attach(T entity);
        int Count();
        int Count(Expression<Func<T, bool>> expressionFunction);
        T FirstOrDefault(Expression<Func<T, bool>> expressionFunction, bool asTracked = false);
        IEnumerable<T> Find(Expression<Func<T, bool>> expressionFunction, bool asTracked = false);
        void Update(T entity);
        PagingResponse<T> Paging(int pageNumber, int pageSize, Expression<Func<T, bool>> expressionFunction = null);
    }
}