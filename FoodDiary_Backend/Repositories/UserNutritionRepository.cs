using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodDiary_Backend.DataAccess;
using FoodDiary_Backend.Models;
using FoodDiary_Backend.Repositories.Interfaces;
using FoodDiary_Backend.Services;

namespace FoodDiary_Backend.Repositories
{
    public class UserNutritionRepository:IUserNutritionRepository
    {
        private DbContext _context;

        public UserNutritionRepository(DbContext context)
        {
            _context = context;
        }

        public IList<UserNutrition> GetAllUserProductsByDate(int userId, DateTime date)
        {
            using (var command = _context.CreateCommand())
            {
                List<UserNutrition> listOfUserNutrition = new List<UserNutrition>();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "selectProdOfUserByDay";
                command.Parameters.Add(command.CreateParameter("@user_id", userId));
                command.Parameters.Add(command.CreateParameter("@date", date.Date));

                using (var record = command.ExecuteReader())
                {
                    while (record.Read())
                    {
                        Product prToAdd = new Product()
                        {
                            CategoryName = record.GetString(0),
                            SubcategoryName = record.GetString(1),
                            Name = record.GetString(2),
                            CaloriesIn100G = Math.Round(float.Parse(record.GetDouble(3).ToString()),2),
                            FatIn100G = Math.Round(float.Parse(record.GetDouble(4).ToString()),2),
                            ProteinIn100G = Math.Round(float.Parse(record.GetDouble(5).ToString()),2),
                            CarbohydratesIn100G = Math.Round(float.Parse(record.GetDouble(6).ToString()),2)
                        };

                        listOfUserNutrition.Add(new UserNutrition()
                        {
                            Product=prToAdd,
                            NumberOfGrams = record.GetInt32(7),
                            DateOfNutrition = date
                        });
                    }
                    return listOfUserNutrition;
                }
            }
        }

        public IDictionary<DateTime, float> GetSumsOfCalories(int userId, int numOfDays)
        {
            using (var command = _context.CreateCommand())
            {
                Dictionary<DateTime, float> listOfCalories = new Dictionary<DateTime, float>();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "selectSumsOfCalories";
                command.Parameters.Add(command.CreateParameter("@user_id", userId));
                command.Parameters.Add(command.CreateParameter("@numOfDays", numOfDays));

                using (var record = command.ExecuteReader())
                {
                    while (record.Read())
                    {
                        listOfCalories.Add(record.GetDateTime(0), float.Parse(record.GetDouble(1).ToString()));
                    }
                    return listOfCalories;
                }
            }
        }

        public IDictionary<DateTime, float> GetSumsOfFat(int userId, int numOfDays)
        {
            using (var command = _context.CreateCommand())
            {
                Dictionary<DateTime, float> listOfCalories = new Dictionary<DateTime, float>();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "selectSumsOfFat";
                command.Parameters.Add(command.CreateParameter("@user_id", userId));
                command.Parameters.Add(command.CreateParameter("@numOfDays", numOfDays));

                using (var record = command.ExecuteReader())
                {
                    while (record.Read())
                    {
                        listOfCalories.Add(record.GetDateTime(0), float.Parse(record.GetDouble(1).ToString()));
                    }
                    return listOfCalories;
                }
            }
        }

        public IDictionary<DateTime, float> GetSumsOfProtein(int userId, int numOfDays)
        {
            using (var command = _context.CreateCommand())
            {
                Dictionary<DateTime, float> listOfCalories = new Dictionary<DateTime, float>();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "selectSumsOfProtein";
                command.Parameters.Add(command.CreateParameter("@user_id", userId));
                command.Parameters.Add(command.CreateParameter("@numOfDays", numOfDays));

                using (var record = command.ExecuteReader())
                {
                    while (record.Read())
                    {
                        listOfCalories.Add(record.GetDateTime(0), float.Parse(record.GetDouble(1).ToString()));
                    }
                    return listOfCalories;
                }
            }
        }

        public IDictionary<DateTime, float> GetSumsOfCarbohydrates(int userId, int numOfDays)
        {
            using (var command = _context.CreateCommand())
            {
                Dictionary<DateTime, float> listOfCalories = new Dictionary<DateTime, float>();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "selectSumsOfCarbo";
                command.Parameters.Add(command.CreateParameter("@user_id", userId));
                command.Parameters.Add(command.CreateParameter("@numOfDays", numOfDays));

                using (var record = command.ExecuteReader())
                {
                    while (record.Read())
                    {
                        listOfCalories.Add(record.GetDateTime(0), float.Parse(record.GetDouble(1).ToString()));
                    }
                    return listOfCalories;
                }
            }
        }

        public int AddNewProductToNutrition(int userId, DateTime date, string productName, int number)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "addProductToNutrition";
                command.Parameters.Add(command.CreateParameter("@userId", userId));
                command.Parameters.Add(command.CreateParameter("@dat", date.Date));
                command.Parameters.Add(command.CreateParameter("@prodName", productName));
                command.Parameters.Add(command.CreateParameter("@number", number));

                using (var record = command.ExecuteReader())
                {
                    int numOfInsertedRanges = record.RecordsAffected;

                    return numOfInsertedRanges;
                }
            }
        }

        public bool UpdateProductInNutrition(int userId, DateTime date, string subcategoryName, string productName, int changedNumber)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "updateProductInNutrition";
                command.Parameters.Add(command.CreateParameter("@userId", userId));
                command.Parameters.Add(command.CreateParameter("@dat", date.Date));
                command.Parameters.Add(command.CreateParameter("@prodName", productName));
                command.Parameters.Add(command.CreateParameter("@number", changedNumber));
                command.Parameters.Add(command.CreateParameter("@productSubcat", subcategoryName));

                using (var record = command.ExecuteReader())
                {
                    int numOfInsertedRanges = record.RecordsAffected;
                    
                    if (numOfInsertedRanges > 0)
                    {
                        return true;
                    }

                    return false;
                }
            }
        }

        public bool DropProductInNutrition(int userId, DateTime date, string productName)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dropProductInNutrition";
                command.Parameters.Add(command.CreateParameter("@userId", userId));
                command.Parameters.Add(command.CreateParameter("@dat", date.Date));
                command.Parameters.Add(command.CreateParameter("@prodName", productName));
                
                using (var record = command.ExecuteReader())
                {
                    int numOfInsertedRanges = record.RecordsAffected;
                    
                    if (numOfInsertedRanges > 0)
                    {
                        return true;
                    }

                    return false;
                }
            }
        }
    }
}
