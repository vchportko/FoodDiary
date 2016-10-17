using System.Data;
using FoodDiary_Backend.DataAccess;
using FoodDiary_Backend.Repositories.Interfaces;
using FoodDiary_Backend.Services;

namespace FoodDiary_Backend.Repositories
{
    public class UserRepository:IUserRepository
    {
        private DbContext _context;

        public UserRepository(DbContext context)
        {
            _context = context;
        }

        public int GetUserByLogin(string login, string password)
        {
            using (var command = _context.CreateCommand())
            {
                int userId = -1;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "loginUser";
                command.Parameters.Add(command.CreateParameter("@login", login));
                command.Parameters.Add(command.CreateParameter("@password", password));

                using (var record = command.ExecuteReader())
                {
                    while (record.Read())
                    {
                        userId = record.GetInt32(0);
                        break;
                    }
                    return userId;
                }
            }
        }
    }
}
