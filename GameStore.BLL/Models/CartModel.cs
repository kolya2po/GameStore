using GameStore.DAL.Entities;
using System.Collections.Generic;

namespace GameStore.BLL.Models
{
    public class CartModel : BaseModel
    {
        public int TotalItems { get; set; }

        public IEnumerable<CartItem> CartItems { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
