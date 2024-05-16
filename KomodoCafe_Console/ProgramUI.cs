using MenuRepository.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace KomodoCafe.Console
{
    class ProgramUI
    {
        private MenuRepo _menuRepository;

        public ProgramUI()
        {
            _menuRepository = new MenuRepo();
            SeedMenuItems();
        }

        public void Run()
        {
            bool continueToRun = true;
            while (continueToRun)
            {
                DisplayMenuItems(); // Display the menu at the start of each iteration
                System.Console.WriteLine("Enter the option number:\n" +
                                          "1. Add Menu Item\n" +
                                          "2. Delete Menu Item\n" +
                                          "3. Update Menu Item\n" +
                                          "4. Exit");
                string input = System.Console.ReadLine();
                switch (input)
                {
                    case "1":
                        AddMenuItem();
                        break;
                    case "2":
                        DeleteMenuItem();
                        break;
                    case "3":
                        UpdateMenuItem();
                        break;
                    case "4":
                        continueToRun = false;
                        break;
                    default:
                        System.Console.WriteLine("Please enter a valid number.");
                        break;
                }
            }
        }

        private void AddMenuItem()
        {
            System.Console.WriteLine("Enter the meal number:");
            int mealNumber = int.Parse(System.Console.ReadLine());
            System.Console.WriteLine("Enter the meal name:");
            string mealName = System.Console.ReadLine();
            System.Console.WriteLine("Enter the description:");
            string description = System.Console.ReadLine();
            System.Console.WriteLine("Enter ingredients (comma separated):");
            List<string> ingredients = System.Console.ReadLine().Split(',').Select(i => i.Trim()).ToList();
            System.Console.WriteLine("Enter the price:");
            decimal price = decimal.Parse(System.Console.ReadLine());
            MenuItem newItem = new MenuItem(mealNumber, mealName, description, ingredients, price);
            _menuRepository.AddMenuItem(newItem);
            System.Console.WriteLine("Menu item added.");
        }

        private void DeleteMenuItem()
        {
            System.Console.WriteLine("Enter the meal number of the item to delete:");
            int mealNumber = int.Parse(System.Console.ReadLine());
            if (_menuRepository.DeleteMenuItem(mealNumber))
            {
                System.Console.WriteLine("Item deleted.");
            }
            else
            {
                System.Console.WriteLine("Item not found.");
            }
        }

        private void DisplayMenuItems()
        {
            var items = _menuRepository.GetAllMenuItems();
            if (items.Count == 0)
            {
                System.Console.WriteLine("No menu items found.");
            }
            else
            {
                System.Console.WriteLine("Menu Items:");
                foreach (var item in items)
                {
                    System.Console.WriteLine($"Meal Number: {item.MealNumber}");
                    System.Console.WriteLine($"Meal Name: {item.MealName}");
                    System.Console.WriteLine($"Description: {item.Description}");
                    System.Console.WriteLine($"Ingredients: {string.Join(", ", item.Ingredients)}");
                    System.Console.WriteLine($"Price: ${item.Price:F2}");
                    System.Console.WriteLine("-----------------------------------");
                }
            }
        }

        private void UpdateMenuItem()
        {
            System.Console.WriteLine("Enter the meal number of the item to update:");
            int mealNumber = int.Parse(System.Console.ReadLine());
            var existingItem = _menuRepository.GetMenuItemByNumber(mealNumber);
            if (existingItem == null)
            {
                System.Console.WriteLine("Item not found.");
                return;
            }

            System.Console.WriteLine("Enter new meal name (leave blank to keep current):");
            string mealName = System.Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(mealName))
            {
                existingItem.MealName = mealName;
            }

            System.Console.WriteLine("Enter new description (leave blank to keep current):");
            string description = System.Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(description))
            {
                existingItem.Description = description;
            }

            System.Console.WriteLine("Enter new ingredients (comma-separated, leave blank to keep current):");
            string ingredientsInput = System.Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(ingredientsInput))
            {
                existingItem.Ingredients = ingredientsInput.Split(',').Select(i => i.Trim()).ToList();
            }

            System.Console.WriteLine("Enter new price (leave blank to keep current):");
            string priceInput = System.Console.ReadLine();
            if (decimal.TryParse(priceInput, out decimal newPrice))
            {
                existingItem.Price = newPrice;
            }

            System.Console.WriteLine("Menu item updated.");
        }

        private void SeedMenuItems()
        {
            var items = new List<MenuItem>
            {
                new MenuItem(1, "Burger", "Delicious beef burger", new List<string>{"Beef", "Bun", "Lettuce", "Tomato", "Cheese"}, 5.99m),
                new MenuItem(2, "Pizza", "Cheese and tomato pizza", new List<string>{"Dough", "Tomato Sauce", "Cheese"}, 8.99m),
                new MenuItem(3, "Salad", "Fresh vegetable salad", new List<string>{"Lettuce", "Tomato", "Cucumber", "Onion"}, 4.99m)
            };

            foreach (var item in items)
            {
                _menuRepository.AddMenuItem(item);
            }
        }
    }
}