using System.Collections.Generic;
using FoodDiary_Backend.Models;

namespace FoodDiary_Backend.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        IList<Category> GetAllCategories(string contain);

        bool AddNewCategory(string newCategoryName);
    }
}
