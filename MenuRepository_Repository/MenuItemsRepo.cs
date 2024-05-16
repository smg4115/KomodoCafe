namespace MenuRepository.Repository;

public class MenuRepo
{
    private List<MenuItem> _menuItems = new List<MenuItem>();

    // Add a new menu item
    public void AddMenuItem(MenuItem menuItem)
    {
        _menuItems.Add(menuItem);
    }

    // Delete a menu item by meal number
    public bool DeleteMenuItem(int mealNumber)
    {
        MenuItem item = GetMenuItemByNumber(mealNumber);
        if (item != null)
        {
            _menuItems.Remove(item);
            return true;
        }
        return false;
    }

    // Get all menu items
    public List<MenuItem> GetAllMenuItems()
    {
        return _menuItems;
    }

    // Get a menu item by meal number
    public MenuItem GetMenuItemByNumber(int mealNumber)
    {
        return _menuItems.FirstOrDefault(item => item.MealNumber == mealNumber);
    }

    // Update a menu item
    public bool UpdateMenuItem(int originalMealNumber, MenuItem newItem)
    {
        MenuItem oldItem = GetMenuItemByNumber(originalMealNumber);
        if (oldItem != null)
        {
            oldItem.MealName = newItem.MealName;
            oldItem.Description = newItem.Description;
            oldItem.Ingredients = newItem.Ingredients;
            oldItem.Price = newItem.Price;
            return true;
        }
        return false;
    }
}