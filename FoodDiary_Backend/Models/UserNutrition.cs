using System;

namespace FoodDiary_Backend.Models
{
    public class UserNutrition
    {
        public DateTime DateOfNutrition { get; set; }

        public Product Product { get; set; }

        public int NumberOfGrams { get; set; }
    }
}
