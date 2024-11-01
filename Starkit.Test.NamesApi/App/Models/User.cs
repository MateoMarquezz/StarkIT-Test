namespace App.Models
{
    public class User
    {
        public string name { get; set; }
        public string gender { get; set; }
    }

    public class UserResponse
    {
        public List<User> response { get; set; }
    }
}

