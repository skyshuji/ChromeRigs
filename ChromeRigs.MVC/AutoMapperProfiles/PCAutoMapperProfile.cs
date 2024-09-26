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

            // Mapping from CreateUpdatePCViewModel to PC
            CreateMap<CreateUpdatePCViewModel, PC>();

            // Mapping from PC to CreateUpdatePCViewModel
            CreateMap<PC, CreateUpdatePCViewModel>()
    .ForMember(createUpdatePCViewModel => createUpdatePCViewModel.ComponentsIds,
        opts => opts.MapFrom(pc => pc.Components != null
            ? pc.Components.Select(component => component.Id)
            : new List<int>()));

        }
    }
}
