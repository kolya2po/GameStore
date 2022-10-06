namespace GameStore.DAL.Entities.Order
{
    public class OrderItem : BaseEntity
    {
        public string GameName { get; set; }
        public decimal GamePrice { get; set; }
        public string GameDescription { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
    }
}
