using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic;


//Обобщенный интерфейс
public interface ICollectable<T>
{
    void Add( T item);
    bool Remove(T item);
    void ViewAll();
    T Find (Predicate<T> predicate);
}

public class Production
{
    public int Id {get; set;}
    public string OrganizationName {get; set;}
    public Production(int id, string organizationName)
    {
        Id = id;
        OrganizationName = organizationName;
    }

    public override string ToString()
    {
        return $"Production: ID={Id}, Organization={OrganizationName}";
    }
}

//Обобщенный класс
public class CollectionType<T> : ICollectable<T> where T : class
{
    private List<T> collection;
    public CollectionType()
    {
        collection = new List<T>();
    }

    public void Add(T item)
    {
        collection.Add(item);
        Console.WriteLine($"Добавлен элемент {item}");
    }
    public bool Remove(T item)
    {
       if (collection.Remove(item))
        {
            Console.WriteLine($"{item} удален");
            return true;
        }
        else
        {
            Console.WriteLine("элемент не найден");
            return false;
        }
    }
    public void ViewAll()
    {
        Console.WriteLine("Элементы коллекции");
        for (int i = 0; i < collection.Count; i++)
        {
            Console.WriteLine(collection[i]);
        }
    }
    public T Find(Predicate<T> predicate)
    {
        T result = collection.Find(predicate);
        if (result != null) Console.WriteLine($"Элемент {result} найден");
        else Console.WriteLine("Элемент не найден");
        return result;
    }
    public void SaveToFile(string filename)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (T item in collection)
                {
                    writer.WriteLine(item.ToString());
                }
            }
            Console.WriteLine("Сохранено в файл");
        } catch (Exception ex)
        {
            Console.WriteLine("Ошибка при сохранении");
        }
        finally
        {
            Console.WriteLine("Операция сохранения завершена");
        }
    }
    public void LoadFromFile(string filename)
    {
        try
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("Файл не существует");
                return;
            }
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        } catch (Exception ex)
        {
            Console.WriteLine("Ошибка загрузки");
        } finally
        {
            Console.WriteLine("Операция загрузки завершена");
        }
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Тип string:");
        CollectionType<string> stringCollection = new CollectionType<string>();
        stringCollection.Add("Apple Inc.");
        stringCollection.Add("Microsoft Corp.");
        stringCollection.Add("Google LLC");
        stringCollection.ViewAll();
        stringCollection.Find(s => s.Contains("Apple"));
        stringCollection.SaveToFile("strings.txt");
        stringCollection.Remove("Google LLC");

        Console.WriteLine("Тип Int");
        CollectionType<object> intCollection = new CollectionType<object>();
        intCollection.Add(10);
        intCollection.Add(20);
        intCollection.Add(30);
        intCollection.ViewAll();

        Console.WriteLine("Тип Production");
        CollectionType<Production> productionCollection = new CollectionType<Production>();
        productionCollection.Add(new Production(1, "Tesla Inc."));
        productionCollection.Add(new Production(2, "Microsoft corp."));
        productionCollection.Find(p => p.Id == 2);
        productionCollection.Remove(new Production(1, "Tesla Inc."));
        productionCollection.ViewAll();

        stringCollection.LoadFromFile("strings.txt");
    }
}