using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KennelIndexer.API.Helpers
{
    public class Uploader
    {
        public string UploadPictures(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            var fileExtension = Path.GetExtension(files.FirstOrDefault().FileName);
            var fileName = Guid.NewGuid() + fileExtension;
            var fullPath = Path.Combine(pathToSave, fileName);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {

                    using (var stream = System.IO.File.Create(fullPath))
                    {
                         formFile.CopyToAsync(stream);
                    }
                }
            }

            return fullPath;
        }
    }
}
