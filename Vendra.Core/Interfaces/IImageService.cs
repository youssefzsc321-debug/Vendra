using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendra.Core.Interfaces
{
    public interface IImageService
    {
        Task<(bool IsUploaded, string? ErrorMessage)> UploadAsync(IFormFile image, string imagName, string folderPath, bool hasThumbnail);


        void Delete(string imagePath, string? thumnailPath = null);
    }
}
