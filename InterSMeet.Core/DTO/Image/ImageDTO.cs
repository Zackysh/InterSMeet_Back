using InterSMeet.DAL.Entities;
using Microsoft.AspNetCore.Http;

namespace InterSMeet.Core.DTO
{
    public class ImageDTO
    {
        public string ImageTitle { get; set; } = null!;
        public byte[] ImageData { get; set; } = null!;

        public static ImageDTO FronIFormFile(IFormFile image)
        {
            ImageDTO imageDto = new();
            MemoryStream ms = new();
            image.CopyTo(ms);
            imageDto.ImageData = ms.ToArray();
            imageDto.ImageTitle = image.FileName;

            ms.Close();
            ms.Dispose();

            return imageDto;
        }

        public static ImageDTO FromImage(Image image)
        {
            return new()
            {
                ImageTitle = image.ImageTitle,
                ImageData = image.ImageData
            };
        }
    }
}
