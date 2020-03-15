using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using KennelIndexer.API.Models;
using KennelIndexer.API.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KennelIndexer.API.Controllers
{
    [Route("api/pictures")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly IPersonLibraryRepository _personLibraryRepository;
        private readonly IMapper _mapper;

        private readonly string[] ACCEPTED_FILE_TYPES = new[] { ".jpg", ".jpeg", ".png" };

        public PicturesController(IPersonLibraryRepository personLibraryRepository, IMapper mapper)
        {
            _personLibraryRepository = personLibraryRepository ??
                throw new ArgumentNullException(nameof(personLibraryRepository));

            _mapper = mapper ??
                 throw new ArgumentNullException(nameof(mapper));

        }
        [HttpGet("{personId}", Name ="GetPictures")]
        public IActionResult GetPictures(Guid personId)
        {
            var picturesFromRepo = _personLibraryRepository.GetPictures(personId);
            return Ok(_mapper.Map<IEnumerable<PictureDto>>(picturesFromRepo));
        }
        [HttpGet("{personId}/{pictureId}", Name = "GetPicture")]
        public IActionResult GetPicture(Guid personId, Guid pictureId)
        {
            try
            {
                var pictureFromRepo = _personLibraryRepository.GetPicture(personId, pictureId);

                if (pictureFromRepo == null)
                {
                    const string errorMessage = "Picture could not be found.";
                    return NotFound(errorMessage);
                }
                return Ok(_mapper.Map<PictureDto>(pictureFromRepo));
            }
            catch(Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }

        [HttpPost("~/api/UploadPictures")]
        public async Task<IActionResult> UploadPictures(List<IFormFile> files, PersonForCreationDto person)
        {
            //var personEntity = _mapper.Map<Entities.Person>(person);
            //var personId = personEntity.PersonId;

            //if(!_personLibraryRepository.PersonExists(personId))
            //{
            //    _personLibraryRepository.AddPerson(personEntity);


            //}
            //_personLibraryRepository.AddPicture(personEntity.PersonId)
            //var uploader = new Helpers.Uploader();
            //var pictureUrl = await uploader.UploadPictures(files);

            //return Ok(new { pictureUrl });
            //if (!_personLibraryRepository.PersonExists(personId))
            //{
            //    return NotFound();
            //}


            long size = files.Sum(f => f.Length);

            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

           

            var fileName = Guid.NewGuid() + ".jpg";
            var fullPath = Path.Combine(pathToSave, fileName);

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

            return Ok(new { count = files.Count, size, fullPath });
        }
    }
}