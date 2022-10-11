using GameStore.BLL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.WebApi.Models.Cart
{
    public class UpdateCartDto
    {
        [Required]
        public int Id { get; set; }
        public IEnumerable<CartItemModel> CartItems { get; set; }
    }
}
