using AutoMapper;
using ChromeRigs.Entities.Orders;
using ChromeRigs.MVC.Models.Orders;

namespace ChromeRigs.MVC.AutoMapperProfiles
{
    public class OrderAutoMapperProfile : Profile
    {
        public OrderAutoMapperProfile()
        {

            CreateMap<Order, OrderViewModel>();
            CreateMap<Order, OrderDetailsViewModel>();

            CreateMap<Order, OrderViewModel>()
                .ForMember(dest => dest.CustomerFullName,
                           opts => opts.MapFrom(src => src.Customer.FullName));

            CreateMap<Order, OrderDetailsViewModel>();

            CreateMap<CreateUpdateOrderViewModel, Order>();


        }

    }
}
