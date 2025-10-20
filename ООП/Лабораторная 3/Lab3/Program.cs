using System;
using System.Linq;
using System.Text;

// Вложенный объект Production
public class Production
{
    public int Id { get; set; }
    public string OrganizationName { get; set; }
    public Production(int id, string organizationName)
    {
        Id = id;
        OrganizationName = organizationName;
    }
}

// Вложенный класс Developer
public class Developer
{
    public string FullName { get; set; }
    public int Id { get; set; }
    public string Department { get; set; }
    
    public Developer(string fullName, int id, string department)
    {
        FullName = fullName;
        Id = id;
        Department = department;
    }
}

// Основной класс Set
public class Set
{
    private int[] elements;
    public int Count { get; private set; }
    public Production ProductionInfo { get; set; }
    public Developer DeveloperInfo { get; set; }
    public Set(int[] initialElements)
    {
        var distinctElements = initialElements.Distinct().ToArray();
        elements = distinctElements;
        Count = distinctElements.Length;
        ProductionInfo = new Production(1, "Set Organization Inc.");
        DeveloperInfo = new Developer("John Smith", 101, "Software Development");
    }
    public int this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();
            return elements[index];
        }
    }
    private bool Contains(int element)
    {
        for (int i = 0; i < Count; i++)
        {
            if (elements[i] == element)
                return true;
        }
        return false;
    }
    
    // ПЕРЕГРУЖЕННЫЕ ОПЕРАЦИИ
    //добавить элемент в множество
    public static Set operator +(Set set, int item)
    {
        if (set.Contains(item))
        {
            int[] copyElements = new int[set.Count];
            Array.Copy(set.elements, copyElements, set.Count);
            return new Set(copyElements);
        }
        int[] newElements = new int[set.Count + 1];
        Array.Copy(set.elements, newElements, set.Count);
        newElements[set.Count] = item;
        return new Set(newElements);
    }
    
    //объединение множеств
    public static Set operator +(Set set1, Set set2)
    {
        int[] unionElements = new int[set1.Count + set2.Count];
        Array.Copy(set1.elements, unionElements, set1.Count);
        int currentIndex = set1.Count;
        for (int i = 0; i < set2.Count; i++)
        {
            if (!set1.Contains(set2[i]))
            {
                unionElements[currentIndex++] = set2[i];
            }
        }
        int[] resultElements = new int[currentIndex];
        Array.Copy(unionElements, resultElements, currentIndex);
        return new Set(resultElements);
    }
    
    //пересечение множеств
    public static Set operator *(Set set1, Set set2)
    {
        int maxPossibleIntersection = Math.Min(set1.Count, set2.Count);
        int[] intersectionElements = new int[maxPossibleIntersection];
        int count = 0;
        for (int i = 0; i < set1.Count; i++)
        {
            if (set2.Contains(set1[i]))
            {
                intersectionElements[count++] = set1[i];
            }
        }
        int[] resultElements = new int[count];
        Array.Copy(intersectionElements, resultElements, count);
        return new Set(resultElements);
    }
    
    //явный int() - мощность множества
    public static explicit operator int(Set set)
    {
        return set.Count;
    }
    
    //проверка на принадлежность размера массива определенному диапазону
    public static bool operator false(Set set)
    {
        return set.Count < 5 || set.Count > 20;
    }
    public static bool operator true(Set set)
    {
        return set.Count >= 5 && set.Count <= 20;
    }
    public override string ToString()
    {
        if (Count == 0) return "Set: {}";
        StringBuilder sb = new StringBuilder("Set: {");
        for (int i = 0; i < Count; i++)
        {
            sb.Append(elements[i]);
            if (i < Count - 1) sb.Append(", ");
        }
        sb.Append("}");
        return sb.ToString();
    }
}

// Статический класс StatisticOperation
public static class StatisticOperation
{
    public static int Sum(Set set)
    {
        int sum = 0;
        for (int i = 0; i < (int)set; i++)
        {
            sum += set[i];
        }
        return sum;
    }
    public static int MaxMinDifference(Set set)
    {
        if ((int)set == 0) return 0;
        int max = set[0];
        int min = set[0];
        for (int i = 1; i < (int)set; i++)
        {
            if (set[i] > max) max = set[i];
            if (set[i] < min) min = set[i];
        }
        return max - min;
    }
    public static int ElementCount(Set set)
    {
        return (int)set;
    }
}

// Методы расширения для типа string
public static class StringExtensions
{
    public static string AddCommaAfterEachWord(this string str)
    {
        if (string.IsNullOrEmpty(str)) return str;
        string[] words = str.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return string.Join(", ", words);
    }
    public static Set RemoveDuplicates(this Set set)
    {
        int[] elements = new int[set.Count];
        for (int i = 0; i < set.Count; i++)
        {
            elements[i] = set[i];
        }
        return new Set(elements);
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== ТЕСТИРОВАНИЕ КЛАССА SET ===");
        Console.WriteLine();
        Set set1 = new Set(new int[] { 1, 2, 3, 4, 5 });
        Set set2 = new Set(new int[] { 4, 5, 6, 7, 8 });
        set1.DeveloperInfo = new Developer("Homan", 101, "Department");
        Console.WriteLine(set1.DeveloperInfo.FullName);
        // Тестирование перегруженных операций
        Console.WriteLine();
        Console.WriteLine("Перегруженные операции");
        //добавить элемент в множество
        Set set3 = set1 + 10;
        Console.WriteLine("set1 + 10 = " + set3.ToString());
        //объединение множеств
        Set union = set1 + set2;
        Console.WriteLine("set1 + set2 = " + union.ToString());
        //пересечение множеств
        Set intersection = set1 * set2;
        Console.WriteLine("set1 * set2 = " + intersection.ToString());
        //мощность множества
        Console.WriteLine("Мощность set1: " + (int)set1);
        Console.WriteLine("Мощность set2: " + (int)set2);
        //проверка диапазона
        Console.WriteLine();
        Console.WriteLine("Проверка диапазона размера (5-20 элементов)");
        // Тестирование вложенных объектов
        Console.WriteLine();
        Console.WriteLine("Вложенные объекты");
        Console.WriteLine("Production: ID=" + set1.ProductionInfo.Id + ", Organization=" + set1.ProductionInfo.OrganizationName);
        Console.WriteLine("Developer: " + set1.DeveloperInfo.FullName + ", ID=" + set1.DeveloperInfo.Id + ", Department=" + set1.DeveloperInfo.Department);
        // Тестирование StatisticOperation
        Console.WriteLine();
        Console.WriteLine("StatisticOperation");
        Console.WriteLine("Сумма элементов set1: " + StatisticOperation.Sum(set1));
        Console.WriteLine("Разница макс-мин set1: " + StatisticOperation.MaxMinDifference(set1));
        Console.WriteLine("Количество элементов set1: " + StatisticOperation.ElementCount(set1));
        // Тестирование методов расширения
        Console.WriteLine();
        Console.WriteLine("Методы расширения");
        string testString = "Это тестовая строка";
        Console.WriteLine("Исходная строка: " + testString);
        Console.WriteLine("С запятыми: " + testString.AddCommaAfterEachWord());
        Set setWithDuplicates = new Set(new int[] { 1, 2, 2, 3, 3, 3, 4 });
        Console.WriteLine("Множество с дубликатами: " + setWithDuplicates.ToString());
        Set setWithoutDuplicates = setWithDuplicates.RemoveDuplicates();
        Console.WriteLine("Без дубликатов: " + setWithoutDuplicates.ToString());
        Console.WriteLine();
    }
}