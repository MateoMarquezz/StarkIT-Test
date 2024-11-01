using App.Models;

public interface IUserServices
{
    List<User> GetAllUsers();
    List<User>? GetFilteredUsers(string? gender, string? startWith);
}
