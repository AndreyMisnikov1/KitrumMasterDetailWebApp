namespace BL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.Extensions.ExpressionMapping;

    using BL.Interfaces;

    using DAL.Entities;
    using DAL.Interfaces;
    using DAL.Objects;

    public abstract class GenericDbServiceBase<TEntity, TDomainObject> : IGenericService<TDomainObject>
        where TEntity : HasIdBase<int>, new() where TDomainObject : HasIdBase<int>
    {
        /// <summary>
        /// The repository.
        /// </summary>
        protected readonly IGenericRepository<TEntity> Repository;

        /// <summary>
        /// The mapper.
        /// </summary>
        protected readonly IMapper Mapper;

        protected GenericDbServiceBase(IGenericRepository<TEntity> repository, IMapper mapper)
        {
            this.Repository = repository;
            this.Mapper = mapper;
        }

        public Task<int> CountAsync()
        {
            return this.Repository.CountAsync();
        }

        public List<TDomainObject> GetPage(int pageIndex, int pageSize)
        {
            var customers = this.Repository.GetPageAsync(pageIndex, pageSize).Result;
            return this.Mapper.Map<List<TDomainObject>>(customers);
        }

        public TDomainObject FindById(int id)
        {
            var customer = this.Repository.FindById(id);
            return this.Mapper.Map<TDomainObject>(customer);
        }

        public bool Any(int id)
        {
            return this.Repository.Any(id);
        }

        public Task<int> Insert(TDomainObject entity)
        {
            var tEntity = this.Mapper.Map<TEntity>(entity);
            return this.Repository.Insert(tEntity);
        }

        public Task<int> Update(TDomainObject entity)
        {
            var tEntity = this.Mapper.Map<TEntity>(entity);
            return this.Repository.Update(tEntity);
        }

        public IEnumerable<TDomainObject> GetPage(int pageIndex, int pageSize, Expression<Func<TDomainObject, bool>> predicate, out int count)
        {
            var efExpression = this.Mapper.MapExpression<Expression<Func<TEntity, bool>>>(predicate);
            IEnumerable<TEntity> customers = this.Repository.GetPage(pageIndex, pageSize, efExpression, out count);
            return this.Mapper.Map<IEnumerable<TEntity>, List<TDomainObject>>(customers);
        }

        public void Dispose()
        {
            this.Repository.Dispose();
        }
    }
}
