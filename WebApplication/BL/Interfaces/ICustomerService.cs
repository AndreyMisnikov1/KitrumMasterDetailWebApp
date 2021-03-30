namespace BL.Interfaces
{
    using System.Collections.Generic;

    using BL.DTO;

    public interface ICustomerService : IGenericService<CustomerDTO>
    {
        public List<CustomerDTO> GetPage(int pageIndex, int pageSize, string searchString, out int count);
    }
}
