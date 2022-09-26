namespace GameStore.BLL.Models.Identity
{
    public class UserModel : BaseModel
    {
        public string AvatarImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
