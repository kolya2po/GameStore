using System.Collections.Generic;

namespace GameStore.DAL.Entities.Order
{
    public class Order : BaseEntity
    {
        public IEnumerable<OrderItem> OrderItems { get; set; }
        public ContactInformation ContactInformation { get; set; }
        public string Comments { get; set; }
        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
