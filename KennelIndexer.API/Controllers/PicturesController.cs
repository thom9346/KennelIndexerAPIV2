using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using KennelIndexer.API.Models;
using KennelIndexer.API.Services;
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

        public PicturesController(IPersonLibraryRepository personLibraryRepository, IMapper mapper)
        {
            _personLibraryRepository = personLibraryRepository ??
                throw new ArgumentNullException(nameof(personLibraryRepository));

            _mapper = mapper ??
                 throw new ArgumentNullException(nameof(mapper));
        }
        public IActionResult GetPictures(Guid personId)
        {
            var picturesFromRepo = _personLibraryRepository.GetPictures(personId);
            return Ok(_mapper.Map<IEnumerable<PictureDto>>(picturesFromRepo));
        }

        public object GetPicture(Guid personId, Guid pictureId)
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
    }
}