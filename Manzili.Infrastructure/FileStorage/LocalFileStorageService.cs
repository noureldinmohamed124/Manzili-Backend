using Manzili.Application.Abstractions.FileStorage;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Infrastructure.FileStorage
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _environment;

        public LocalFileStorageService( IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveImageAsync(Stream stream, string fileName, string folderName)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", folderName);

            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";

            var fullPath = Path.Combine(uploadsFolder, uniqueFileName);

            using var fileStream = new FileStream(fullPath, FileMode.Create);

            await stream.CopyToAsync(fileStream);

            return $"/uploads/{folderName}/{uniqueFileName}";
        }
    }
}
