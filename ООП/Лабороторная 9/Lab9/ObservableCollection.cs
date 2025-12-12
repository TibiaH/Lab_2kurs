using System.Collections.ObjectModel;

public class ObservableCollectionDemo
{
    private static void OnCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        Console.WriteLine($"\nСобытие: {e.Action}");
        
        if (e.NewItems != null)
        {
            foreach (Product item in e.NewItems)
            {
                Console.WriteLine($"Добавлен: {item}");
            }
        }
        
        if (e.OldItems != null)
        {
            foreach (Product item in e.OldItems)
            {
                Console.WriteLine($"Удален: {item}");
            }
        }
    }
    
    public static void Demo()
    {
        Console.WriteLine("\n=== ЗАДАНИЕ 3: ObservableCollection<Product> ===");
        
        ObservableCollection<Product> products = new ObservableCollection<Product>();
        products.CollectionChanged += OnCollectionChanged;
        
        Console.WriteLine("\nДобавление:");
        products.Add(new Product("TV", 499.99m));
        products.Add(new Product("Microwave", 129.99m));
        
        Console.WriteLine("\nВставка:");
        products.Insert(1, new Product("Radio", 39.99m));
        
        Console.WriteLine("\nУдаление по индексу:");
        products.RemoveAt(0);
        
        Console.WriteLine("\nУдаление:");
        products.Remove(new Product("Radio", 39.99m));
        
        Console.WriteLine("\nОчистка:");
        products.Clear();
        
        Console.WriteLine("\nДобавление после очистки:");
        products.Add(new Product("Watch", 199.99m));
    }
}