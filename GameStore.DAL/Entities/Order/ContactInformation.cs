using System.ComponentModel.DataAnnotations;

namespace GameStore.DAL.Entities.Order
{
    public class ContactInformation : BaseEntity
    {
        public int OrderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }
    }
}
