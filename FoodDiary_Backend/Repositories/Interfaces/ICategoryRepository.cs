using System.Collections.Generic;
using FoodDiary_Backend.Models;

namespace FoodDiary_Backend.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories(string contain);

        bool AddNewCategory(string newCategoryName);
    }
}
