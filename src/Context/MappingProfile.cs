using AutoMapper;
using TrainingRestFullApi.src.DTOs.Movie;
using TrainingRestFullApi.src.Entities;

namespace TrainingRestFullApi.src.Context
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Movie, MUpdateDTO>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<MUpdateDTO, Movie>().ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
