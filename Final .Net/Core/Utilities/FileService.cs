using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities
{
    public class FileService :IFileService
    {
        public async Task<string> Upload(IFormFile file,string webRootPath)
        {
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var path = Path.Combine(webRootPath, "assets/images", fileName);
            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                await file.CopyToAsync(fileStream);
            }
            return fileName;
        }
        public void Delete(string webRootPath,string fileName)
        {
            var path = Path.Combine(webRootPath, "assets/images", fileName);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        public bool IsImage(IFormFile file)
        {
            if (file.ContentType.Contains("image/")) return true;
            return false;
        }

        public bool CheckSize(IFormFile file, int size)
        {
           return !(file.Length / 1024 > size);
        }
    }
}
