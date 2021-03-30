namespace BL.EFProfile
{
    using AutoMapper;

    using BL.DTO;

    using DAL.Objects;

    public class EFProfile : Profile
    {
        public EFProfile()
        {
            this.CreateMap<CustomerDTO, CustomerEntity>().ReverseMap();
        }
    }
}
