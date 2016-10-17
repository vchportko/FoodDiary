using System.Data;

namespace FoodDiary_Backend.DataAccess
{
    public class DbContext
    {
        private readonly IDbConnection _connection;
        private readonly IConnectionFactory _connectionFactory;


        public DbContext(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            _connection = _connectionFactory.Create();
        }

        public IDbCommand CreateCommand()
        {
            var cmd = _connection.CreateCommand();

            return cmd;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
