using DB.Models;
using DoublevPartnersWebAPI.Models.Inputs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Utils;

namespace DoublevPartnersWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DbDoubleVpartnersContext _context;

        public UserController(DbDoubleVpartnersContext context)
        {
            _context = context;
        }

        [HttpGet(Constants.GetUsers)]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _context.Users.ToListAsync();

            return Ok(result);
        }

        [HttpPost(Constants.SaveUser)]
        public async Task<IActionResult> SaveUser([FromBody] UserInput userInput)
        {
            if (userInput == null)
            {
                return BadRequest(Constants.InvalidInputData);
            }

            try
            {
                var newUser = new User
                {
                    UserName = userInput.UserName,
                    Pass = HashPassword(userInput.Pass),
                    CreationDate = userInput.CreationDate ?? DateTime.UtcNow
                };

                _context.Users.Add(newUser);

                await _context.SaveChangesAsync();

                return Ok(Constants.UserSuccessSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error saving person: {ex.Message}");
            }
        }

        [HttpPost(Constants.ValidateUser)]
        public async Task<IActionResult> ValidateUser([FromBody] UserValidationInput userInput)
        {
            if (userInput == null || string.IsNullOrEmpty(userInput.UserName) || userInput.Pass == null)
            {
                return BadRequest(Constants.InvalidInputData);
            }

            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.UserName == userInput.UserName);

                if (user != null && VerifyPassword(userInput.Pass, user.Pass))
                {
                    return Ok(Constants.UserValidationSuccess);
                }
                else
                {
                    return BadRequest(Constants.UserValidationFailed);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error validating user: {ex.Message}");
            }
        }

        private bool VerifyPassword(string inputPassword, byte[] storedHashedPassword)
        {
            using (var sha512 = SHA512.Create())
            {
                var hashedInputPassword = sha512.ComputeHash(Encoding.UTF8.GetBytes(inputPassword));
                return hashedInputPassword.SequenceEqual(storedHashedPassword);
            }
        }

        private byte[] HashPassword(string password)
        {
            using (var sha256 = SHA512.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
