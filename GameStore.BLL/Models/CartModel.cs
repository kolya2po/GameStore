using System.Collections.Generic;

namespace GameStore.BLL.Models
{
    public class CartModel : BaseModel
    {
        public int TotalItems { get; set; }

        public IEnumerable<CartItemModel> CartItems { get; set; }

        public decimal TotalPrice { get; set; }
        public string UserName { get; set; }
    }
}
