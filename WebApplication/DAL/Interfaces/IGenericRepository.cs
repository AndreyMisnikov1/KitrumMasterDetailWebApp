namespace DAL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    public interface IGenericRepository<TEntity>
        where TEntity : class, IHasId<int>
    {
        Task<int> CountAsync();
        Task<List<TEntity>> GetPageAsync(int pageIndex, int pageSize);
        IEnumerable<TEntity> GetAll();
        TEntity FindById(int id);
        IEnumerable<TEntity> Find(Func<TEntity, Boolean> predicate);
        Task<int> Insert(TEntity item);
        Task<int> Update(TEntity item);
        void Delete(int id);
        bool Any(int id);
        IEnumerable<TEntity> GetPage(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> predicate, out int count);
        public void Dispose();
    }
}
