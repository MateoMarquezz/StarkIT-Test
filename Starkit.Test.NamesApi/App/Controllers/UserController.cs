using App.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [ApiController]
    [Route("api/names")]
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserServices userServices, ILogger<UserController> logger)
        {
            _userServices = userServices;
            _logger = logger;
        }

        /// <summary>
        /// Filter by name and gender
        /// </summary>
        /// <param name="Name"></param>
        /// <returns>Returns OK if filtered correctly, otherwise logs are recorded.</returns>
        [HttpGet]
        public IActionResult GetUsers([FromQuery] string? gender, [FromQuery] string? startWith)
        {
            _logger.LogInformation("Received request to get users with gender: {Gender} and startWith: {StartWith}", gender, startWith);

            if (string.IsNullOrEmpty(gender) && string.IsNullOrEmpty(startWith))
            {
                var allUsers = _userServices.GetAllUsers();
                return Ok(allUsers);
            }

            var filteredUsers = _userServices.GetFilteredUsers(gender, startWith);

            if (filteredUsers == null)
            {
                _logger.LogWarning("No valid filter was added. Please try again with a valid filter or leave it empty.");

                return BadRequest("No valid filter was added. Please try again with a valid filter or leave it empty.");
            }

            _logger.LogInformation("Returning filtered users: {Count}", filteredUsers.Count);
            return Ok(filteredUsers);
        }
    }
}
