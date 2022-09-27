using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Models;
using GameStore.WebApi.Models.Games;
using GameStore.WebApi.Models.Genres;

namespace GameStore.WebApi.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateGameDto, GameModel>().ReverseMap();
            CreateMap<UpdateGameDto, GameModel>().ReverseMap();

            CreateMap<CreateGenreDto, GenreModel>().ReverseMap();
            CreateMap<UpdateGenreDto, GenreModel>().ReverseMap();
        }
    }
}
