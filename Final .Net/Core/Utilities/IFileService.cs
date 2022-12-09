using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities
{
    public interface IFileService
    {
        Task<string> Upload(IFormFile file, string webRootPath);
        void Delete(string webRootPath, string fileName);
       bool IsImage(IFormFile file);
        bool CheckSize(IFormFile file, int size);
    }
}
