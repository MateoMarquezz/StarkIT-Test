using App.Models;
using System.Text.Json;

namespace App.DataAccess
{
    public class UserRepository
    {
        private List<User> _users;
        private readonly string _jsonFilePath; 

        public UserRepository(string jsonFilePath)
        {
            _jsonFilePath = jsonFilePath; 
            LoadUsers();
        }

        private void LoadUsers()
        {
            var json = File.ReadAllText(_jsonFilePath);
            var userResponse = JsonSerializer.Deserialize<UserResponse>(json);

            _users = userResponse?.response ?? new List<User>();
        }

        public List<User> GetAllUsers()
        {
            return _users;
        }
    }
}
