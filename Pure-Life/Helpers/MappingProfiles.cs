using AutoMapper;
using Pure_Life.Models;
using Pure_Life.ViewModel.Stafi;

namespace Pure_Life.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AddStafiViewModel, Stafi>();
         


        }
    }
}