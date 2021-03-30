namespace DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using DAL.Interfaces;
    using DAL.Objects;

    public class CustomerRepository : GenericRepository<CustomerEntity>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<CustomerEntity> GetPage(int pageIndex, int pageSize, Expression<Func<CustomerEntity, bool>> predicate, out int count)
        {
            var result = this.databaseSet.Where(predicate);
            count = result.ToList().Count;
            return result.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
