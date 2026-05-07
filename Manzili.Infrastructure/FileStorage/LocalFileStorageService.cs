using Manzili.Application.Abstractions.FileStorage;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public async Task<string> SaveImageAsync(Stream stream, string fileName, string folderName, string? serviceTitle)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", folderName);

            Directory.CreateDirectory(uploadsFolder);

            string uniqueName = "";

            if (serviceTitle != null)
            {
                serviceTitle = Regex.Replace(serviceTitle ?? "", @"[^a-zA-Z0-9_]", "");
                uniqueName = $"{serviceTitle}_";
            }

            string guid = Guid.NewGuid().ToString("N")[..8];
            uniqueName += guid;

            var uniqueFileName = $"{uniqueName}{Path.GetExtension(fileName)}";

            var fullPath = Path.Combine(uploadsFolder, uniqueFileName);

            using var fileStream = new FileStream(fullPath, FileMode.Create);

            await stream.CopyToAsync(fileStream);

            return $"/uploads/{folderName}/{uniqueFileName}";
        }
    }
}
