using System;
using System.IO;
using System.Threading.Tasks;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Interfaces;
using Microsoft.AspNetCore.Http;

namespace GameStore.BLL.Services
{
    public class ImagesService : IImagesService
    {
        public async Task<string> SaveImageAsync(IFormFile image, string pathToFolder, HttpRequest request)
        {
            ValidateFile(image);

            if (!Directory.Exists(pathToFolder))
            {
                Directory.CreateDirectory(pathToFolder);
            }

            var imageFormat = image.FileName.Split('.')[^1];

            var imageName = $"{Guid.NewGuid()}.{imageFormat}";
            var filePath = Path.Combine(pathToFolder, imageName);

            await using var stream = new FileStream(filePath, FileMode.Create);

            await image.CopyToAsync(stream);

            var domainName = $"{request.Scheme}://{request.Host.Value}";

            return $"{domainName}/{imageName}";
        }

        private static void ValidateFile(IFormFile file)
        {
            if (file == null)
            {
                throw new GameStoreException("Image was null.");
            }

            if (!file.ContentType.Contains("image"))
            {
                throw new GameStoreException("You should send an image.");
            }
        }
    }
}
