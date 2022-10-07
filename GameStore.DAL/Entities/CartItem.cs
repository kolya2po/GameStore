namespace GameStore.DAL.Entities
{
    public class CartItem
    {
        public int GameId { get; set; }
        public Game Game { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }
    }
}
