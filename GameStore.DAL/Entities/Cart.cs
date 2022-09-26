using System.Collections.Generic;
using System.Linq;

namespace GameStore.DAL.Entities
{
    public class Cart : BaseEntity
    {
        public IEnumerable<CartItem> CartItems { get; set; }
        public decimal TotalPrice => CartItems.Sum(c => c.Quantity * c.Game.Price);
        public int UserId { get; set; }
    }
}
