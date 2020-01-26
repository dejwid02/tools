using AutoMapper;
using MovieParser.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.API
{
    public class MoviesProfile : Profile
    {
        public MoviesProfile()
        {
            CreateMap<Movie, Data.Movie>().ReverseMap();
            CreateMap<Actor, Data.Actor>().ReverseMap();
            CreateMap<Director, Data.Director>().ReverseMap();
            CreateMap<Recording, Data.Recording>().ReverseMap();
            CreateMap<TvListingItem, Data.TvListingItem>().ReverseMap();
        }
    }
}
