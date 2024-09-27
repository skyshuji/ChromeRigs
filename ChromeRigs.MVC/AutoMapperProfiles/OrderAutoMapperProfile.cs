using AutoMapper;
using ChromeRigs.Entities.Orders;
using ChromeRigs.MVC.Models.Orders;

namespace ChromeRigs.MVC.AutoMapperProfiles
{
    public class OrderAutoMapperProfile : Profile
    {
        public OrderAutoMapperProfile()
        {

            CreateMap<Order, OrderViewModel>()
                .ForMember(dest => dest.CustomerFullName,
                           opts => opts.MapFrom(src => src.Customer.FullName));

            CreateMap<Order, OrderDetailsViewModel>();

            CreateMap<CreateUpdateOrderViewModel, Order>();

            CreateMap<Order, CreateUpdateOrderViewModel>()
                .ForMember(createUpdateOrderViewModel => createUpdateOrderViewModel.PCIds,
                      opts =>
                        opts.MapFrom(Order => Order.PCs.Select(pc => pc.Id))
                );



        }

    }
}
