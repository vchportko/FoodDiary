using System.Collections.Generic;
using FoodDiary_Backend.Models;

namespace FoodDiary_Backend.Repositories.Interfaces
{
    public interface ISubcategoryRepository
    {
        IList<Subcategory> GatAllSubcategories(string categoryName, string contain);

        bool AddNewSubcategory(string categoryName, string newSubcategoryName);
    }
}
