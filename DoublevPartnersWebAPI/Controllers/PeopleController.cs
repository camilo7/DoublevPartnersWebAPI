using DB.Models;
using DoublevPartnersWebAPI.Models.Inputs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utils;

namespace DoublevPartnersWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly DbDoubleVpartnersContext _context;

        public PeopleController(DbDoubleVpartnersContext context)
        {
            _context = context;
        }

        [HttpGet(Constants.GetPeople)]
        public async Task<IActionResult> GetPeople()
        {
            var result = await _context.People.FromSqlRaw(Constants.GetPeople).ToListAsync();

            return Ok(result);
        }

        [HttpPost(Constants.SavePerson)]
        public async Task<IActionResult> SavePerson([FromBody] PeopleInput peopleInput)
        {
            if (peopleInput == null)
            {
                return BadRequest(Constants.InvalidInputData);
            }

            try
            {
                var newPerson = new Person
                {
                    Names = peopleInput.Names,
                    LastNames = peopleInput.LastNames,
                    IdentificationNumber = peopleInput.IdentificationNumber,
                    Email = peopleInput.Email,
                    IdentificationType = peopleInput.IdentificationType,
                    CreationDate = peopleInput.CreationDate ?? DateTime.UtcNow
                };

                _context.People.Add(newPerson);

                await _context.SaveChangesAsync();

                return Ok(Constants.PersonSuccessSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error saving person: {ex.Message}");
            }
        }

    }
}
