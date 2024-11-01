using App.DataAccess;
using App.Models;

namespace App.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserRepository _userRepository;
        private readonly ILogger<UserServices> _logger; 

        public UserServices(UserRepository repository, ILogger<UserServices> logger) 
        {
            _logger = logger; 
            _userRepository = repository;
        }
        
        public List<User> GetAllUsers()
        {
            _logger.LogInformation("Fetching all users");
            return _userRepository.GetAllUsers();
        }

        
        public List<User>? GetFilteredUsers(string? gender, string? startWith)
        {
            _logger.LogInformation("Fetching filtered users with gender: {Gender} and startWith: {StartWith}", gender, startWith);

            bool isGenderValid = string.IsNullOrEmpty(gender) || gender.Equals("M", StringComparison.OrdinalIgnoreCase) || gender.Equals("F", StringComparison.OrdinalIgnoreCase);
            bool isStartWithValid = string.IsNullOrEmpty(startWith) || startWith.All(char.IsLetter);

            if (!isGenderValid && !isStartWithValid)
            {
                _logger.LogWarning("Both filters are invalid: gender '{Gender}' and startWith '{StartWith}'.", gender, startWith);
                return null;
            }
            else if (!isGenderValid)
            {
                _logger.LogWarning("Invalid gender filter provided: '{Gender}'.", gender);
                return null;
            }
            else if (!isStartWithValid)
            {
                _logger.LogWarning("Invalid startWith filter provided: '{StartWith}'.", startWith);
                return null;
            }


            var users = GetAllUsers().AsQueryable(); 

            if (isGenderValid && !string.IsNullOrEmpty(gender))
            {
                users = users.Where(x => x.gender.Equals(gender, StringComparison.OrdinalIgnoreCase));
            }

            if (isStartWithValid && !string.IsNullOrEmpty(startWith))
            {
                users = users.Where(x => x.name.StartsWith(startWith, StringComparison.OrdinalIgnoreCase));
            }

            var filteredUsers = users.ToList();
            _logger.LogInformation("Filtered users found: {Count}", filteredUsers.Count);
            return filteredUsers;
        }

    }
}
