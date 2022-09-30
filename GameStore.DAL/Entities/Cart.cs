using System.Collections.Generic;

namespace GameStore.DAL.Entities
{
    public class Cart : BaseEntity
    {
        public IEnumerable<CartItem> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
