﻿namespace GameStore.DAL.Entities.Order
{
    public class ContactInformation : BaseEntity
    {
        public int OrderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
