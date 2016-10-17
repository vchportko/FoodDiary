namespace FoodDiary_Backend.Models
{
    public class Product
    {
        public string CategoryName { get; set; }

        public string SubcategoryName { get; set; }

        public string Name { get; set; }
        
        public double CaloriesIn100G { get; set; }

        public double FatIn100G { get; set; }

        public double ProteinIn100G { get; set; }

        public double CarbohydratesIn100G { get; set; }
    }
}
