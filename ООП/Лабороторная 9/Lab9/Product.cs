using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;

public class Product : IOrderedDictionary
{
    public string Name {get; set;}
    public decimal Price {get; set;}

    public Product() {}
    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public object this[int index]
    {
        get => index == 0 ? Name : Price;
        set
        {
            if (index == 0) Name = (string)value;
            else Price = (decimal)value;
        }
    }

    public object this[object key]
    {
        get => key.ToString() == "Name" ? Name : Price;
        set
        {
            if (key.ToString() == "Name") Name = (string)value;
            else Price = (decimal)value;
        }
    }

    public ICollection Keys => new string[] {"Name", "Price"};
    public ICollection Values => new object[] {Name, Price};
    public bool IsReadOnly => false;
    public bool IsFixedSize => false;
    public int Count => 2;
    public bool IsSynchronized => false;
    public object SyncRoot => this;

    public IDictionaryEnumerator GetEnumerator() => null;
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => null;

    public void Add(object key, object value) {}
    public void Clear() {}
    public bool Contains(object key) => true;
    public void Remove(object key) {}
    public void CopyTo(Array array, int index) {}
    public void Insert(int index, object key, object value) {}
    public void RemoveAt(int index) {}

    public override string ToString()
    {
        return $"Name: {Name}, Price: {Price}";
    }
}