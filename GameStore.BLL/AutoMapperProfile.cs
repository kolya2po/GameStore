using System.Linq;
using System.Threading;
using AutoMapper;
using GameStore.BLL.Models;
using GameStore.BLL.Models.Identity;
using GameStore.DAL.Entities;
using GameStore.DAL.Entities.Order;

namespace GameStore.BLL
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Game, GameModel>()
                .ForMember(dst => dst.Genres, opt => opt.MapFrom(src => src.Genres.Select(c => c.Genre.Name)))
                .ForMember(dst => dst.Comments, opt => opt.MapFrom(src => src.Comments.Where(c => c.ParentCommentId == null)));

            CreateMap<GameModel, Game>()
                .ForMember(dst => dst.Genres, opt => opt.Ignore());

            CreateMap<Genre, GenreModel>().ReverseMap();

            CreateMap<User, UserModel>().ReverseMap();

            CreateMap<Comment, CommentModel>()
                .ForMember(dst => dst.CreationDate,
                    opt =>
                    opt.MapFrom(src =>
                       src.CreationDate.ToString(Thread.CurrentThread.CurrentCulture)))
                .ForMember(dst => dst.Author,
                    opt =>
                        opt.MapFrom(src =>
                            src.Author.UserName))
                .ReverseMap();

            CreateMap<CommentModel, Comment>()
                .ForMember(dst => dst.Author, opt =>
                    opt.Ignore());

            CreateMap<CartItem, CartItemModel>().ReverseMap();
            CreateMap<CartItemModel, CartItem>()
                .ForMember(dst => dst.Game, opt => opt.Ignore());

            CreateMap<Cart, CartModel>()
                .ForMember(dst => dst.TotalItems, opt =>
                    opt.MapFrom(src => src.CartItems.Count()))
                .ForMember(dst => dst.TotalPrice, opt =>
                    opt.MapFrom(src => src.CartItems.Sum(c => c.Game.Price * c.Quantity)))
                .ReverseMap();

            CreateMap<Game, GameCartModel>().ReverseMap();

            CreateMap<Order, OrderModel>()
                .ForMember(dst => dst.PaymentType, opt => 
                    opt.MapFrom(c => c.PaymentType.Name))
                .ReverseMap();

            CreateMap<OrderModel, Order>()
                .ForMember(dst => dst.PaymentType, opt => opt.Ignore());

            CreateMap<CartItemModel, OrderItem>()
                .ForMember(dst => dst.Id, opt =>
                    opt.Ignore())
                .ForMember(dst => dst.GameName, opt =>
                    opt.MapFrom(c => c.Game.Name))
                .ForMember(dst => dst.GameDescription, opt =>
                    opt.MapFrom(c => c.Game.Description))
                .ForMember(dst => dst.GamePrice, opt =>
                    opt.MapFrom(c => c.Game.Price))
                .ReverseMap();
        }
    }
}
