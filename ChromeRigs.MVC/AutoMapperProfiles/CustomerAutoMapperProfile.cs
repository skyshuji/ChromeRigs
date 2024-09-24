using AutoMapper;
using ChromeRigs.Entities.Customers;
using ChromeRigs.MVC.Models.Customers;

namespace ChromeRigs.MVC.AutoMapperProfiles
{
    public class CustomerAutoMapperProfile : Profile
    {
        public CustomerAutoMapperProfile()
        {
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<Customer, CustomerDetailsViewModel>();
            CreateMap<CreateUpdateCustomerViewModel, Customer>().ReverseMap();
        }

    }
}
