using System.Collections.Concurrent;

class Program
{
    static void Main(string[] args)
    {

        //Задание 1
        var collection = new ProductCollection();

        collection.AddProduct(new Product("macBook", 4000m));
        collection.AddProduct(new Product ("iphone", 3400m));
        collection.DisplayAllProduct();

        collection.RemoveProduct("iphone");
        collection.DisplayAllProduct();

        //Задание 2
        ConcurrentBag<int> bag = new ConcurrentBag<int>();

        bag.Add(10);
        bag.Add(20);
        bag.Add(30);

        foreach (var item in bag)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();

        List<int> tempList = new List<int>();
        foreach (var item in bag)
        {
            tempList.Add(item);
        }

        tempList.RemoveAt(2);

        bag = new ConcurrentBag<int>();
        foreach (var item in tempList)
        {
            bag.Add(item);
        }

        foreach (var item in bag)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();

        Dictionary<int, string> dict = new Dictionary<int, string>();
        int key = 1;
        foreach (var item in bag)
        {
            dict.Add(key, item.ToString());
            key++;
        }

        foreach (var kvp in dict)
        {
            Console.WriteLine($"Key: {kvp.Key}, value: {kvp.Value}");
        }

        
    }
}