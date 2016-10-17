using System.Data;

namespace FoodDiary_Backend.DataAccess
{
    public interface IConnectionFactory
    {
        IDbConnection Create();
    }
}
