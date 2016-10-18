using System.Collections.Generic;
using System.Data;
using FoodDiary_Backend.DataAccess;
using FoodDiary_Backend.Models;
using FoodDiary_Backend.Services;

namespace FoodDiary_Backend.Repositories.Interfaces
{
    public  class CategoryRepository:ICategoryRepository
    {
        private readonly DbContext _context;

        public CategoryRepository(DbContext context)
        {
            _context = context;
        }

        public List<Category> GetAllCategories(string contain)
        {
            using (var command = _context.CreateCommand())
            {
                List<Category> listOfCategoties=new List<Category>();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "selectCategories";
                command.Parameters.Add(command.CreateParameter("@category", contain));
                
                using (var record = command.ExecuteReader())
                {
                    while (record.Read())
                    {
                        listOfCategoties.Add(new Category() {Name = record.GetString(0)});
                    }
                    return listOfCategoties;
                }
            }
        }

        public bool AddNewCategory(string newCategoryName)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "addCategory";
                command.Parameters.Add(command.CreateParameter("@catName", newCategoryName));

                using (var record = command.ExecuteReader())
                {
                    int numOfInsertedRanges=0;
                    while (record.Read())
                    {
                        numOfInsertedRanges = record.GetInt32(0);
                    }

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
