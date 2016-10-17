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
    public class SubcategoryRepository:ISubcategoryRepository
    {
        private DbContext _context;

        public SubcategoryRepository(DbContext context)
        {
            _context = context;
        }


        public IList<Subcategory> GatAllSubcategories(string categoryName, string contain)
        {
            using (var command = _context.CreateCommand())
            {
                List<Subcategory> listOfSubcategoties = new List<Subcategory>();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "selectSubcategoriesByCategory";
                command.Parameters.Add(command.CreateParameter("@category", categoryName));
                command.Parameters.Add(command.CreateParameter("@subcategory", contain));

                using (var record = command.ExecuteReader())
                {
                    while (record.Read())
                    {
                        listOfSubcategoties.Add(new Subcategory(){ Name = record.GetString(0) });
                    }
                    return listOfSubcategoties;
                }
            }
        }

        public bool AddNewSubcategory(string categoryName, string newSubcategoryName)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "addSubcategory";
                command.Parameters.Add(command.CreateParameter("@catName", categoryName));
                command.Parameters.Add(command.CreateParameter("@subcatName", newSubcategoryName));

                using (var record = command.ExecuteReader())
                {
                    int numOfInsertedRanges = 0;
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
