using System.Collections.Concurrent;

public class ProductCollection
{
    private ConcurrentBag<Product> products = new ConcurrentBag<Product>();
    public void AddProduct(Product product)
    {
        products.Add(product);
        Console.WriteLine($"Product - {product.Name} added");
    }

    public bool RemoveProduct(string name)
    {
        var newBag = new ConcurrentBag<Product>();
        bool removed = false;

        foreach (var product in products)
        {
            if (product.Name != name)
            {
                newBag.Add(product);
            }
            else
            {
                removed = true;
            }
        }

        products = newBag;
        return removed;
    }

    public Product FindProduct(string name)
    {
        foreach (var product in products)
        {
            if (product.Name == name)
            return product;
        }
        return null;
    }

    public void DisplayAllProduct()
    {
        foreach (var product in products)
        {
            Console.WriteLine(product);
        }
    }
}
