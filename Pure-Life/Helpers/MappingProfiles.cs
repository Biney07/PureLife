using AutoMapper;
using Pure_Life.Models;
using Pure_Life.ViewModel;

namespace Pure_Life.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<StafiViewModel, Stafi>();
         


        }
    }
}