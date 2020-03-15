using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using BrunoZell.ModelBinding;
using KennelIndexer.API.Models;
using KennelIndexer.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KennelIndexer.API.Controllers
{
    [Route("api/people")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonLibraryRepository _personLibraryRepositry;
        private readonly IMapper _mapper;

        public PeopleController(IPersonLibraryRepository personLibraryRepository, IMapper mapper)
        {
            _personLibraryRepositry = personLibraryRepository ??
                throw new ArgumentNullException(nameof(personLibraryRepository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetPeople()
        {
            //This is the entity: Person.cs
            var peopleFromRepo = _personLibraryRepositry.GetPeople();
            //we map it to the model: PersonDto asnd return it.
            return Ok(_mapper.Map<IEnumerable<PersonDto>>(peopleFromRepo));
        }
        [HttpGet("{personId}", Name = "GetPerson")]
        public IActionResult GetPerson(Guid personId)
        {
            try
            {
                var personFromRepo = _personLibraryRepositry.GetPerson(personId);

                if (personFromRepo == null)
                {
                    const string errorMessage = "Person was not found";
                    return NotFound(errorMessage);
                }

                return Ok(_mapper.Map<PersonDto>(personFromRepo));
            }
            catch(Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public ActionResult<PersonDto> PostPerson(
            [FromForm] PersonForCreationDto person, List<IFormFile> files)
        {
            var personEntity = _mapper.Map<Entities.Person>(person); //Maps PersonForCreationDto to Entites.Person. This is possible because of the mapping in PeopleProfile.cs
            _personLibraryRepositry.AddPerson(personEntity, files);
            _personLibraryRepositry.Save();

            var personToReturn = _mapper.Map<PersonDto>(personEntity);

            return CreatedAtRoute("GetPerson",
                new { personId = personToReturn.PersonId },
                personToReturn);
        }
    }
}