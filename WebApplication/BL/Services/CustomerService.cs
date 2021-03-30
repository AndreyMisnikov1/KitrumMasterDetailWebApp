namespace BL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.Extensions.ExpressionMapping;

    using BL.DTO;
    using BL.Interfaces;

    using DAL.Interfaces;
    using DAL.Objects;

    using LinqKit;

    public class CustomerService : GenericDbServiceBase<CustomerEntity, CustomerDTO>, ICustomerService
    { 
        private readonly ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository repository, IMapper mapper) : base(repository, mapper)
        {
            this.customerRepository = repository;
        }

        public List<CustomerDTO> GetPage(int pageIndex, int pageSize, string searchString, out int count)
        {
            IEnumerable<CustomerEntity> customers = this.customerRepository.GetPage(pageIndex, pageSize, this.BuildExpressionToSearchByFields(searchString), out count);
            return this.Mapper.Map<IEnumerable<CustomerEntity>, List<CustomerDTO>>(customers);
        }

        /// <summary>
        /// Build expression to search by Name, Surname, City, Street, Zip
        /// </summary>
        /// <param name="searchString">the value for searching operation (contains) </param>
        /// <returns>Predicate </returns>
        private Expression<Func<CustomerEntity, bool>> BuildExpressionToSearchByFields(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return item => true;
            }

            var predicate = PredicateBuilder.Or<CustomerEntity>(
                    customer => customer.Name != null && customer.Name.Contains(searchString),
                    customer => customer.Surname != null && customer.Surname.Contains(searchString))
                .Or(customer => customer.City != null && customer.City.Contains(searchString))
                .Or(customer => customer.Street != null && customer.Street.Contains(searchString)).Or(
                    customer => customer.Zip != null && customer.Zip.Contains(searchString));
            return predicate;
        }
    }
}
