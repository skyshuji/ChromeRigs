using AutoMapper;
using ChromeRigs.Entities.Components;
using ChromeRigs.MVC.Models.Components;

namespace ChromeRigs.MVC.AutoMapperProfiles
{
    public class ComponentAutoMapperProfile : Profile
    {

        public ComponentAutoMapperProfile()
        {

            CreateMap<Component, ComponentViewModel>();
            CreateMap<Component, ComponentDetailsViewModel>();
            CreateMap<CreateUpdateComponentViewModel, Component>().ReverseMap();

        }

    }
}
