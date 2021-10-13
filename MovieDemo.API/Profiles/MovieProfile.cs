using AutoMapper;
using MovieDemo.API.Models;
using MovieDemo.Entity;
using MovieDemo.Entity.BusinessEntities;

namespace MovieDemo.API.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<MovieEntity, MovieModel>().ReverseMap();
        }
    }
}
