using System.Linq;
using System.Threading;
using AutoMapper;
using GameStore.BLL.Models;
using GameStore.BLL.Models.Identity;
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

            CreateMap<User, UserModel>().ReverseMap();

            CreateMap<Comment, CommentModel>()
                .ForMember(dst => dst.CreationDate,
                    opt =>
                    opt.MapFrom(src =>
                       src.CreationDate.ToString(Thread.CurrentThread.CurrentCulture)))
                .ReverseMap();

            CreateMap<CartItem, CartItemModel>().ReverseMap();

            CreateMap<Cart, CartModel>()
                .ForMember(dst => dst.TotalItems, opt =>
                    opt.MapFrom(src => src.CartItems.Count()))
                .ForMember(dst => dst.TotalPrice, opt =>
                    opt.MapFrom(src => src.CartItems.Sum(c => c.Game.Price * c.Quantity)))
                .ReverseMap();

            CreateMap<Game, GameCartModel>().ReverseMap();
        }
    }
}
