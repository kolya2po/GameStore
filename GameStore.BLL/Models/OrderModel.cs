using GameStore.DAL.Entities.Order;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.BLL.Models
{
    public class OrderModel : BaseModel
    {
        public IEnumerable<OrderItem> OrderItems { get; set; }
        public ContactInformation ContactInformation { get; set; }

        [MaxLength(600)]
        public string Comments { get; set; }
        public string PaymentType { get; set; }
    }
}
