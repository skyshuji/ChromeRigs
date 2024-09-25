using AutoMapper;
using ChromeRigs.Entities.PCs;
using ChromeRigs.MVC.Models.PCs;

namespace ChromeRigs.MVC.AutoMapperProfiles
{
    public class PCAutoMapperProfile : Profile
    {

        public PCAutoMapperProfile()
        {

            CreateMap<PC, PCViewModel>();
            CreateMap<PC, PCDetailsViewModel>();
            CreateMap<CreateUpdatePCViewModel, PC>().ReverseMap();
            CreateMap<PC, CreateUpdatePCViewModel>().ReverseMap()
                .ForMember(createUpdatePCViewModel => createUpdatePCViewModel.ComponentIds,
                    opts =>
                        opts.MapFrom(pc => pc.Components.Select(component => component.Id))
                );

        }

    }
}
