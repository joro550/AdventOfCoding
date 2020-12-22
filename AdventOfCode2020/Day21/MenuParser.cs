using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day21
{
    public static class MenuParser
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
        public List<Ingredient> GetFoodWithNoAllergens()
        {
            var foodWithAllergens = GetFoodWithAllergens().Values.Select(x => x.Name).ToList();

            var ingredientsWithNoAllergens = new List<Ingredient>();
            foreach (var (ingredients, _) in Items)
            {
                ingredientsWithNoAllergens.AddRange(
                    ingredients.Where(ingredient => foodWithAllergens.All(x => x != ingredient.Name)));
            }

            return ingredientsWithNoAllergens.Distinct().ToList();
        }
        
        public Dictionary<string, Ingredient> GetFoodWithAllergens()
        {
            var ingredientMap = new Dictionary<string, List<Ingredient>>();
            foreach (var (ingredients, allergens) in Items)
            {
                foreach (var allergen in allergens)
                {
                    if (ingredientMap.ContainsKey(allergen.Name))
                    {
                        var ingredientList = ingredientMap[allergen.Name];
                        var commonNames = new List<string>();
                        
                        foreach (var ingredient in ingredientMap[allergen.Name])
                        {
                            commonNames.AddRange(from i in ingredients 
                                where ingredient.Name == i.Name 
                                select ingredient.Name);
                        }

                        ingredientList.RemoveAll(x => commonNames.All(n => n != x.Name));

                    }
                    else
                        ingredientMap.Add(allergen.Name, new List<Ingredient>(ingredients));
                }
            }

            while (ingredientMap.Select(x => x.Value).Any(v => v.Count > 1))
            {
                var mapWithOne = ingredientMap.Where(map => map.Value.Count == 1);
                foreach (var (_, value) in mapWithOne)
                {
                    var toRemove = value.Single();
                    foreach (var key in ingredientMap.Where(map => map.Value.Count > 1).Select(s => s.Key))
                        ingredientMap[key].Remove(toRemove);
                }
            }

            var finalDictionary = new Dictionary<string, Ingredient>();
            foreach (var (key, value) in ingredientMap) 
                finalDictionary.Add(key, value.First());
            return finalDictionary;
        }

        public int GetInstances(IEnumerable<string> names)
        {
            return Items.Sum(item => item.Ingredients.Count(ingredient => names.Any(x => x == ingredient.Name)));;
        }
    }

    public record FoodItem(List<Ingredient> Ingredients, List<Allergen> Allergens)
    {
        public static FoodItem Parse(string item)
        {
            var values = item.Split("(");

            var allergenRegex = new Regex("[a-zA-Z]+");
            var allergens = allergenRegex.Matches(values[1])
                .Select(x => x.Value)
                .Where(x => x != "contains")
                .Select(val => new Allergen(val))
                .ToList();

            var ingredientsList = values[0].Split(" ")
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(ingredient => new Ingredient(ingredient))
                .ToList();

            return new FoodItem(ingredientsList, new List<Allergen>(allergens));
        }
    }

    public record Ingredient(string Name);

    public record Allergen(string Name)
    {
    }

}