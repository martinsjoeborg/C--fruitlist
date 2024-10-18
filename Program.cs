string filePath = "./saved-fruits.cvs";
//Creates a dictionary variable that stores a key and value object
var fruits = new Dictionary<string, decimal>();

//Gets the allowed fruits from .txt file and stores it in a string variable
string allowedFruits = File.ReadAllText("./allowed-fruits.txt");
//Remove the empty lines in the string and stores in an array of strings
string[] cleanAllowedFruits = allowedFruits.Split(['\n', '\r'], StringSplitOptions.RemoveEmptyEntries);
//Trim the leading whitespaces of each string
for(int i = 0; i < cleanAllowedFruits.Length; i++)
{
    cleanAllowedFruits[i] = cleanAllowedFruits[i].Trim();
}

//Starting message
Console.WriteLine("");
Console.WriteLine("Welcome to the fruit app!");
Console.WriteLine("");
Console.WriteLine("The following fruits are available: ");
foreach(string fruit in cleanAllowedFruits) 
{
    Console.WriteLine(fruit);
}
Console.WriteLine("");
Console.WriteLine("If you want to start from where you left off, type 'continue'. ");
Console.WriteLine("");

//Main loop logic
while (true)
{
    Console.Write("Enter a fruit (or 'done' to finish or 'save' to save your fruits): ");
    string newFruit = Console.ReadLine()!.Trim().ToLower(); //Takes input, trim whitespace, sets all characters to lowercase & stores in variable

    //If user types "done", breaks the main loop
    if (newFruit.Equals("done", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }

    if (newFruit.Equals("save", StringComparison.OrdinalIgnoreCase))
    {
        saveFruits(filePath, fruits);
        if(fruits.Count > 0)
        {
            Console.WriteLine("Your fruits has been saved.");
        }
        break;
    }

    if (newFruit.Equals("continue", StringComparison.OrdinalIgnoreCase))
    {
        fruits = getSavedFruits(filePath);
        Console.WriteLine("Your saved fruits:");
        foreach(var fruit in fruits)
        {
            Console.WriteLine($"{fruit.Key}, {fruit.Value}");
        }
        continue;
    }

    //Checks if newFruit is in the cleanAllowedFruits array, if not, shows message and goes back to enter another fruit
    if (!cleanAllowedFruits.Any(fruit => string.Equals(fruit, newFruit, StringComparison.OrdinalIgnoreCase))) 
    {
        Console.WriteLine("Fruit is not available. Please choose one from the list.");
        continue;
    } 

    //Checks if newFruit is in the fruits dictionary, if yes, shows message and goes back to enter another fruit
    if (fruits.Any(fruit => fruit.Key == newFruit))
    {   
        Console.WriteLine($"{newFruit} has already been entered. Please enter a different fruit."); 
        continue; 
    }

    //Tells user to enter price for fruit. 
    //Checks if input is numerical, if yes, adds the price, if not, shows message and goes back to enter another price
    while (true)
    {
        Console.Write($"Enter the price for {newFruit}: ");
        string? newPrice = Console.ReadLine();

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

//Checks if any fruits has been entered, if yes, writes the fruits, if not, shows messsage
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
    Console.WriteLine("No fruits were entered :(");
}

//Gets saved fruits and returns the dictionary to the main fruits dictionary
static Dictionary<string, decimal> getSavedFruits(string filePath)
{
    using var reader = new StreamReader(filePath);
    var savedFruits = new Dictionary<string, decimal>();

    string[] lines = File.ReadAllLines(filePath);

    foreach (string line in lines)
    {
        string[] parts = line.Split(',');
        string fruitName = parts[0].Trim();
        decimal fruitPrice = decimal.Parse(parts[1].Trim());
        savedFruits[fruitName] = fruitPrice;
    }
    return savedFruits;
}

//To Do: make sure it doesn't add existing fruits in saved-fruits.cvs

static void saveFruits(string filePath, Dictionary<string, decimal> fruits)
{
    var tempFruitList = getSavedFruits(filePath);

    using var writer = new StreamWriter(filePath, append: true);
    if (!File.Exists(filePath))
    {
        writer.WriteLine();
    }

    foreach (var fruit in fruits)
    {
        string key = fruit.Key;
        
        if(!tempFruitList.ContainsKey(key))
        {
            writer.WriteLine($"{fruit.Key}, {fruit.Value}");
        }
    }
}