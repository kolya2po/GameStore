using System.Linq;
using AutoMapper;
using GameStore.BLL.Models;
using GameStore.DAL.Entities;

namespace GameStore.BLL
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Game, GameModel>()
                .ForMember(dst => dst.Genres, opt => opt.MapFrom(src => src.Genres.Select(c => c.Genre.Name)))
                .ReverseMap();

            CreateMap<Genre, GenreModel>().ReverseMap();
        }
    }
}
