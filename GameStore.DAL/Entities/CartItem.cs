namespace GameStore.DAL.Entities
{
    public class CartItem : BaseEntity
    {
        public int GameId { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }
    }
}
