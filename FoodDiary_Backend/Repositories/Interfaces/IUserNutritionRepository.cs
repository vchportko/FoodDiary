using System;
using System.Collections.Generic;
using FoodDiary_Backend.Models;

namespace FoodDiary_Backend.Repositories.Interfaces
{
    public interface IUserNutritionRepository
    {
        IList<UserNutrition> GetAllUserProductsByDate(int userId, DateTime date);

        IDictionary<DateTime, float> GetSumsOfCalories(int userId, int numOfDays);

        IDictionary<DateTime, float> GetSumsOfFat(int userId, int numOfDays);

        IDictionary<DateTime, float> GetSumsOfProtein(int userId, int numOfDays);

        IDictionary<DateTime, float> GetSumsOfCarbohydrates(int userId, int numOfDays);

        int AddNewProductToNutrition(int userId, DateTime date, string productName, int number);

        bool UpdateProductInNutrition(int userId, DateTime date, string productName, int changedNumber);

        bool DropProductInNutrition(int userId, DateTime date, string productName);

    }
}
