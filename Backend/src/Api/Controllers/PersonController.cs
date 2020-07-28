using Common.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Api.ActionFilters;
using Application.Interfaces.Person;
using Application.Dtos.Person;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IPersonValidatorService _personValidatorService;

        public PersonController(IPersonService personService, IPersonValidatorService personValidatorService)
        {
            _personService = personService;
            _personValidatorService = personValidatorService;
        }        
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _personService.GetAll());
        }

        [HttpGet("{id}", Name = "GetPerson")]
        public async Task<IActionResult> GetOne(int id)
        {
            var person = await _personService.GetOne(id);

            if (person == null)
                return NotFound();

            return Ok(person);
        }

        [AuthorizeRoles(Roles.Admin, Roles.Moderator)]
        [HttpPost]        
        public async Task<IActionResult> Create(PersonForSaveDto personForSave)
        {
            _personValidatorService.ValidateForSave(personForSave);

            var person = await _personService.Create(personForSave);

            return CreatedAtRoute("GetPerson", new { controller = "Person", id = person.Id }, person);
        }

        [AuthorizeRoles(Roles.Admin, Roles.Moderator)]
        [HttpPut("{id}")]        
        public async Task<IActionResult> Update(int id, PersonForSaveDto personForUpdate)
        {
            await _personValidatorService.ValidateForSave(id, personForUpdate);

            var person = await _personService.Update(id, personForUpdate);

            return Ok(person);
        }

        [AuthorizeRoles(Roles.Admin, Roles.Moderator)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _personService.Delete(id);

            return Ok();
        }

        [HttpGet("recordNames")]
        public async Task<IActionResult> GetRecordNames()
        {
            return Ok(await _personService.GetRecordNames());
        }

    }
}