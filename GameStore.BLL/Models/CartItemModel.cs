namespace GameStore.BLL.Models
{
    public class CartItemModel
    {
        public int GameId { get; set; }
        public GameCartModel Game { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }
    }
}
