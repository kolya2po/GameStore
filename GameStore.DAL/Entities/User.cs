using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace GameStore.DAL.Entities
{
    public class User : IdentityUser<int>
    {
        public string AvatarImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<Game> CreatedGames { get; set; }
    }
}
