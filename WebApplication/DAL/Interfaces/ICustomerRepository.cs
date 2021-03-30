namespace DAL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using DAL.Objects;

    public interface ICustomerRepository : IGenericRepository<CustomerEntity>
    {
        IEnumerable<CustomerEntity> GetPage(int pageIndex, int pageSize, Expression<Func<CustomerEntity, bool>> predicate, out int count);
    }
}
