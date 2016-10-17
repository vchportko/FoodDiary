using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodDiary_Backend.Models;

namespace FoodDiary_Backend.Repositories.Interfaces
{
    public interface IProductRepository
    {
        IList<Product> GetAllProducts(string subcategoryName, string contain);

        bool AddNewProduct(string newProductName, string subCategoryName, float caloriesIn100G, float fatIn100G, float proteinIn100G, float carbohydrateIn100G);
        
    }
}
