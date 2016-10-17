using FoodDiary_Backend.Models;

namespace FoodDiary_Backend.Repositories.Interfaces
{
    public interface IUserRepository
    {
        int GetUserByLogin(string login, string password);
    }
}
