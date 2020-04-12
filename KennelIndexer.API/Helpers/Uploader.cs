using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using System.Net;
using System.Text;
using System.Net.Sockets;

namespace KennelIndexer.API.Helpers
{
    public class Uploader
    {
        //needs to be async / await or the picture wont upload completely
        /*public async Task<string> UploadPictures()
        {

            var files = Request.Form.Files;
            var folderName = Path.Combine("StaticFiles", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (files.Any(f => f.Length == 0))
            {
                return BadRequest();
            }

            foreach (var file in files)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName); //you can add this path to a list and then return all dbPaths to the client if require

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }*/

        /*    long size = files.Sum(f => f.Length);

            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            var fileExtension = Path.GetExtension(files.FirstOrDefault().FileName);
            var fileName = Guid.NewGuid() + fileExtension;
            var fullPath = Path.Combine(folderName, fileName);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {

                    using (var stream = System.IO.File.Create(fullPath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            return fullPath;
            }*/
        //}
    }
}
