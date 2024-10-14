
var fruits = new Dictionary<string, decimal>();
Console.WriteLine("Welcome to the fruit app!");

while (true)
{
    Console.Write("Enter a fruit (or 'done' to finish): ");
    
    string? newFruit = Console.ReadLine();

    if(newFruit.Length < 4)
    {
        Console.WriteLine("Please enter a fruit with at least 4 letters in it.");
        continue;
    }

    if (newFruit.Equals("done", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }

    bool isDuplicate = false;

    foreach (var fruit in fruits)
    {
        if (newFruit == fruit.Key)
        {
            System.Console.WriteLine($"{newFruit} has already been entered. Please enter a different fruit.");
            isDuplicate = true;
            break;
        }
    }

    if (isDuplicate)
    {
        continue;
    }

    while (true)
    {
        Console.Write($"Enter the price for {newFruit}: ");
        string newPrice = Console.ReadLine();

        if (decimal.TryParse(newPrice, out decimal price))
        {
            fruits.Add(newFruit, price);
            break;
        }
        else
        {
            Console.WriteLine("Invalid price. Please enter a numeric value.");
            continue;
        }
    }
}

if (fruits.Count > 0)
{
    var sortedFruits = fruits.OrderBy(fruit => fruit.Value);

    Console.WriteLine("Fruits sorted by price:");
    foreach (var fruit in sortedFruits)
    {
        Console.WriteLine($"{fruit.Key}: {fruit.Value}kr");
    }
}
else
{
    System.Console.WriteLine("No fruits were entered :(");
}