using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manzili.Application.Abstractions.FileStorage
{
    public interface IFileStorageService
    {
        Task<string> SaveImageAsync(Stream stream, string fileName, string folderName);
    }
}
