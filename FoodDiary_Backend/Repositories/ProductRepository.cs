using System.Collections.Generic;
using System.Data;
using FoodDiary_Backend.DataAccess;
using FoodDiary_Backend.Models;
using FoodDiary_Backend.Repositories.Interfaces;
using FoodDiary_Backend.Services;

namespace FoodDiary_Backend.Repositories
{
    public  class ProductRepository:IProductRepository
    {
        private DbContext _context;

        public ProductRepository(DbContext context)
        {
            _context = context;
        }

        public IList<Product> GetAllProducts(string subcategoryName, string contain)
        {
            using (var command = _context.CreateCommand())
            {
                List<Product> listOfProducts = new List<Product>();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "selectProductsBySubcategory";
                command.Parameters.Add(command.CreateParameter("@subcategory", subcategoryName));
                command.Parameters.Add(command.CreateParameter("@product", contain));

                using (var record = command.ExecuteReader())
                {
                    while (record.Read())
                    {
                        listOfProducts.Add(new Product(){ Name = record.GetString(0), SubcategoryName = record.GetString(1),
                            CaloriesIn100G = record.GetFloat(2), FatIn100G = record.GetFloat(3), ProteinIn100G = record.GetFloat(4), CarbohydratesIn100G = record.GetFloat(5)});
                    }
                    return listOfProducts;
                }
            }
        }

        public bool AddNewProduct(string newProductName, string subCategoryName, float caloriesIn100G, float fatIn100G,
            float proteinIn100G, float carbohydrateIn100G)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "addProduct";
                command.Parameters.Add(command.CreateParameter("@productName", newProductName));
                command.Parameters.Add(command.CreateParameter("@subcategoryName", subCategoryName));
                command.Parameters.Add(command.CreateParameter("@calories_in100g", caloriesIn100G));
                command.Parameters.Add(command.CreateParameter("@fat_in100g", fatIn100G));
                command.Parameters.Add(command.CreateParameter("@protein_in100g", proteinIn100G));
                command.Parameters.Add(command.CreateParameter("@carbo_in100g", carbohydrateIn100G));

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
