namespace GameStore.DAL.Entities
{
    public class CartItem : BaseEntity
    {
        public Game Game { get; set; }
        public int Quantity { get; set; }
    }
}
