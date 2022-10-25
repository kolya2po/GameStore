using System.IO;

namespace GameStore.BLL.Infrastructure
{
    public static class MediaPathHelper
    {
        private static readonly string DirectoryOutOfProjectRoot =
            Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.FullName;

        public static string PathToGamesImages => 
            Path.Combine(DirectoryOutOfProjectRoot, "GameStoreItems");

        public static string PathToUsersAvatars =>
            Path.Combine(PathToGamesImages, "UsersAvatars");
    }
}
