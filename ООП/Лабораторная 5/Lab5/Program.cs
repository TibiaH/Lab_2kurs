using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

// Перечисление 
public enum VehicleStatus
{
    Active,
    Inactive
}

// Структура 

public struct MainthainceRecord
{
    public DateTime MainthainceData;
    public MainthainceRecord(DateTime date)
    {
        MainthainceData = date;
    }
}

// Интерфейс

public interface IVehicle
{
    string Name { get; }
    int Year { get; }
    void DisplayInfo();
}

// Абстрактный класс

public abstract class Vehicle : IVehicle
{
    public string Name { get; protected set; }
    public int Year { get; protected set; }
    public VehicleStatus Status { get; set; }
    protected MainthainceRecord Mainthaince { get; set; }
    protected Vehicle(string name, int year)
    {
        Name = name;
        Year = year;
        Status = VehicleStatus.Active;
        Mainthaince = new MainthainceRecord(DateTime.Now);
    }
    public abstract void DisplayInfo();
}

// Partial класс

public partial class Car : Vehicle
{
    public string Brand { get; private set; }
    public Car(string brand, string model, int year) : base($"{brand} {model}", year)
    {
        Brand = brand;
    }
    public override void DisplayInfo()
    {
        Console.WriteLine($"Автомобиль: {Name}, Год: {Year}, Статус: {Status}");
    }
}

// Класс человека 

public sealed class Human : IVehicle
{
    public string Name { get; private set; }
    public int Year { get; private set; }
    public Human(string name, int birthDate)
    {
        Name = name;
        Year = birthDate;
    }
    public void DisplayInfo()
    {
        Console.WriteLine($"человек {Name}, год рождения: {Year}");
    }
}

// Класс трансформера 

public class Transformer : Vehicle
{
    public int Power { get; private set; }

    public Transformer(string name, int year, int power)
        : base(name, year)
    {
        Power = power;
        Status = VehicleStatus.Active;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Трансформер: {Name}, Мощность: {Power}, Год: {Year}");
    }
}

// Контейнер

public class ArmyContainer
{
    private List<IVehicle> units;
    public ArmyContainer()
    {
        units = new List<IVehicle>();
    }
    public void AddUnit(IVehicle unit) => units.Add(unit);
    public bool RemoveUnit(IVehicle unit) => units.Remove(unit);
    public IVehicle GetUnit(int index)
    {
        if (index >= 0 && index < units.Count)
            return units[index];
        return null;
    }
    /*public void SetUnit(int index, IVehicle unit)
    {
        if (index >= 0 && index < units.Count)
            units[index] = unit;
    }*/
    public int Count => units.Count;
    public void DisplayAll()
    {
        foreach (var unit in units)
        {
            unit.DisplayInfo();
        }
        Console.WriteLine($"Состав армии: {units.Count}");
    }
    public List<IVehicle> GetAllUnits() => new List<IVehicle>(units);
}


// Контроллер 

public class ArmyController
{
    private ArmyContainer army;

    public ArmyController(ArmyContainer army)
    {
        this.army = army;
    }
    public void FindByYear(int year)
    {
        var result = army.GetAllUnits().Where(unit => unit.Year == year).ToList();
        if (result.Count == 0)
        {
            Console.WriteLine("Не найдено");
            return;
        }
        foreach (var unit in result)
        {
            unit.DisplayInfo();
        }
    }
    public void FindTransformersByPower(int minPower)
    {
        var result = army.GetAllUnits()
            .Where(unit => unit is Transformer transformer && transformer.Power >= minPower)
            .Cast<Transformer>()
            .ToList();
        if (result.Count == 0)
        {
            Console.WriteLine("Не найдено");
            return;
        }
        foreach (var transformer in result)
        {
            Console.WriteLine($"{transformer.Name} - Мощность: {transformer.Power}");
        }
    }
}

public class Program
{
    public static void Main()
    {
        var army = new ArmyContainer();
        var garage = new ArmyContainer();
        army.AddUnit(new Car("Toyota", "Camry", 2022));
        army.AddUnit(new Car("Volvo", "XC90", 2024));
        army.AddUnit(new Human("Homan Dmitry", 2007));
        army.AddUnit(new Transformer("transformer", 2000, 800));
        garage.AddUnit(new Car("Mercedes", "s-class", 2005));
        var controller = new ArmyController(army);
        var controller2 = new ArmyController(garage);
        VehicleStatus active = VehicleStatus.Active;
        army.DisplayAll();
        controller2.FindByYear(2005);
        controller.FindByYear(2022);
        controller.FindTransformersByPower(800);
        Console.WriteLine(army.GetUnit(1)?.Name);
    }
}