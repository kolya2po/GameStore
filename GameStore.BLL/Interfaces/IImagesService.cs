using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GameStore.BLL.Interfaces
{
    public interface IImagesService
    {
        Task<string> SaveImageAsync(IFormFile image, string pathToFolder, HttpRequest request);
    }
}
