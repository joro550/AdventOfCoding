using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day21
{
    public class MenuParser
    {
        public static Menu Parse(string input)
        {
            var items = input
                .Split(Environment.NewLine)
                .Select(FoodItem.Parse)
                .ToList();
            return new (items);
        } 
    }

    public record Menu(IEnumerable<FoodItem> Items)
    {
        public Menu FindCommonAllergens()
        {
            var foodItems = new List<FoodItem>();

            foreach (var item in Items)
            {
                var foodItem = item;
                
                foreach (var otherItem in Items.Where(i => i != item))
                {
                    foodItem = foodItem.RemoveCommonIngredients(otherItem);
                }

                foodItems.Add(foodItem);
            }

            return new Menu(foodItems);
        }

        public List<string> GetIngredientsWithNoAllergens()
        {
            var ingrediants = new List<string>();
            foreach (var foodItem in Items)
            {
                ingrediants.AddRange(foodItem.Ingredients.Where(ingredient => !ingredient.Allergen.Any()).Select(x=>x.Name));
            }

            return ingrediants.Distinct().ToList();
        }


        public int CountOccurances(List<string> names)
        {
            int count = 0;
            foreach (var item in Items)
            {
                foreach (var name in names)
                {
                    count += item.Ingredients.Count(ingredient => ingredient.Name == name);
                }
            }

            return count;
        }
        
    }

    public record FoodItem(List<Ingredient> Ingredients)
    {
        public static FoodItem Parse(string item)
        {
            var values = item.Split("(");

            var allergens = new List<Allergen>();
            foreach (var val in values[1].Split(" ").Where(x=> x!= "contains"))
            {
                var allergen = val;
                
                if (val.Contains(","))
                    allergen = allergen.Remove(val.IndexOf(",", StringComparison.Ordinal));
                
                if (val.Contains(")"))
                    allergen = allergen.Remove(val.IndexOf(")", StringComparison.Ordinal));
                
                allergens.Add(new Allergen(allergen));
            }

            var ingredientsList = values[0].Split(" ")
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(ingredient => new Ingredient(ingredient, allergens))
                .ToList();


            return new FoodItem(ingredientsList);
        }

        public FoodItem RemoveCommonIngredients(FoodItem otherItem)
        {
            var foodItems = new List<Ingredient>();
            
            foreach (var ourIngredient in Ingredients)
            {
                Ingredient ingredient = ourIngredient;
                var otherIngredient = otherItem.Ingredients.SingleOrDefault(x => x.Name == ourIngredient.Name);
                
                if (otherIngredient != null) 
                    ingredient = ourIngredient.GetIngredients(otherIngredient);

                foodItems.Add(ingredient);  
            }

            return new FoodItem(foodItems);
        }
    }

    public record Ingredient(string Name, List<Allergen> Allergen)
    {
        public Ingredient GetIngredients(Ingredient otherItem)
        {
            var allergens = new List<Allergen>();
            foreach (var allergen in Allergen)
            {
                allergens.AddRange(from theirAllergens in otherItem.Allergen 
                    where allergen == theirAllergens 
                    select allergen);
            }

            return new Ingredient(Name, allergens);
        }
        
    }
    public record Allergen(string Name);

}