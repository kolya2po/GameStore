using AutoMapper;
using GameStore.BLL.Models;
using GameStore.WebApi.Models.Cart;
using GameStore.WebApi.Models.Comments;
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

            CreateMap<CreateCommentDto, CommentModel>().ReverseMap();
            CreateMap<UpdateCommentDto, CommentModel>().ReverseMap();

            CreateMap<UpdateCartDto, CartModel>().ReverseMap();
        }
    }
}
