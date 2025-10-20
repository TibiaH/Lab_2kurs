 using System;
using System.Reflection;
using System.Text;

//C# types
int someInteger = 666;
short someShort = 666;
long someLong = 666;
ushort somuUnsignedShort = 1;
ulong someUnsignedLong = 1;
sbyte someSByte = 127;
byte someByte = 254;
double someDouble = 66.66;
float someFloat = 66.66F;
decimal someDecimal = 66.66M;
bool someBool = true;
char someChar = '\u0025';
string someString = "some text";
string interpolation = $"some text contain {someInteger}";

object booleanType = false;
object integerType = 666;
object stringType = "String";

var varInteger = 666;
var varDounle = 33.3;
var varString = "String";

//Console output

Console.WriteLine(someChar);

//Console input

Console.WriteLine("Введите целое число : ");
int number = int.Parse(Console.ReadLine());

//Implicit casting of types 

long ImplicitDoouble = someInteger;
Console.WriteLine($"implicit: int {someInteger} to long {ImplicitDoouble}");

//Explicit casting of types 

double ExplicitDouble = 666.7;
int ExplicitInt = (int)ExplicitDouble;
Console.WriteLine($"Explicit: double {ExplicitDouble} to int {ExplicitInt}");

//Conver class 

string numberString = "666";
int convertedInt = Convert.ToInt32(numberString);
Console.WriteLine($"Converted string {numberString} to int {convertedInt}");

//Packing and unpacking 

int valueType = 666;
object packing = valueType;
int unpacking = (int)packing;

//nullable variable

int? nullableInt = null;




//C# String

//Comparison of literals

string literal1 = "Hello";
string literal2 = "World";
string sentence = "some word for testing";
Console.WriteLine($"literal1 compoare to literal2: {literal1.CompareTo(literal2)}");

//Concatenation, copying, 

string Concatenation = literal1 + " " + literal2;
string copied = string.Copy(literal1);
string substring = literal1.Substring(1, 2);
string inserted = literal1.Insert(5, literal2);
Console.WriteLine($"concatenation of literal1 and literal2 = {Concatenation}");
Console.WriteLine($"copied of literal1: {copied}");
Console.WriteLine($"substring (1,2): {substring}");
Console.WriteLine($"Insert literal2 into literal 1: {inserted}");

//removed substring

string removed = inserted.Remove(4, 5);
Console.WriteLine($"removed from inserted : {removed}");

//Separation of a line into words

string[] words = sentence.Split(' ');
Console.WriteLine("Separation of a line");
foreach (var word in words)
{
    Console.WriteLine($" '{word}'");
}

//Empty string

string EmptyString = string.Empty;
string nullString = null;
string spaceString = " ";
Console.WriteLine($"string.isNullOrEmpty: {string.IsNullOrEmpty(EmptyString)}");
Console.WriteLine($"empty.string.Length: {EmptyString.Length}");

//StringBuilder 

StringBuilder sb = new StringBuilder("Hello World");
Console.WriteLine($"Source line: {sb}");
sb.Remove(5, 6);
Console.WriteLine($"after removed: {sb}");
sb.Insert(0, "Start: ");
Console.WriteLine($"after insert: {sb}");
sb.Append(" end ");
Console.WriteLine($"after append: {sb}");

//Arrays

//Two-dimensional array

int[,] matrix = {
    {1,2,3,4},
    {5,6,7,8},
    {9,10,11,12}
};
Console.WriteLine("matrix 3x4: ");
int rows = matrix.GetLength(0);
int cols = matrix.GetLength(1);
for (int i = 0; i < rows; i++)
{
    for (int j = 0; j < cols; j++)
    {
        Console.WriteLine($"{matrix[i, j],4}");
    }
    Console.WriteLine();
}

//One-dimensioanl array

string[] stringArray = { "monday", "sunday", "friday" };
Console.WriteLine("One-Dimensional array");
for (int i = 0; i < stringArray.Length; i++)
{
    Console.WriteLine($" [i] = '{stringArray[i]}' Length: {stringArray[i].Length}");
};
Console.WriteLine($"Lenth of array: {stringArray.Length}");

 Console.Write("\nEnter the position to change (0-3): ");
        int position = int.Parse(Console.ReadLine());
        
        Console.Write("Enter a new value: ");
        string newValue = Console.ReadLine();
        
        if (position >= 0 && position < stringArray.Length)
        {
            stringArray[position] = newValue;
            Console.WriteLine("Array after change:");
            for (int i = 0; i < stringArray.Length; i++)
            {
                Console.WriteLine($"  [{i}] = '{stringArray[i]}'");
            }
        }
        else
        {
            Console.WriteLine("Error!");
        }

//Jagget Array

double[][] jaggedArray = new double[3][];
        jaggedArray[0] = new double[2];
        jaggedArray[1] = new double[3]; 
        jaggedArray[2] = new double[4]; 
Console.WriteLine("Enter the value for jagget array:");
        for (int i = 0; i < jaggedArray.Length; i++)
        {
            Console.WriteLine($"line {i} ({jaggedArray[i].Length} elements):");
            for (int j = 0; j < jaggedArray[i].Length; j++)
            {
                Console.Write($"  element [{i}][{j}]: ");
                jaggedArray[i][j] = double.Parse(Console.ReadLine());
            }
        }
Console.WriteLine("\nJagget Array:");
        for (int i = 0; i < jaggedArray.Length; i++)
        {
            Console.Write($"line {i}: ");
            for (int j = 0; j < jaggedArray[i].Length; j++)
            {
                Console.Write($"{jaggedArray[i][j],8:F2}");
            }
            Console.WriteLine();
        }

//Implicitly typed array

var implicitString = "Hello World";
Console.WriteLine($"\nImplicit array:");
Console.WriteLine($"Type: {implicitString.GetType()}");
Console.WriteLine($"content: {implicitString}");
Console.WriteLine($"Lenth: {implicitString.Length}");

//tuples

var tuple = (42, "Hello", 'A', "World", 123456789Ul);
var tuple2 = (42, "Hello", 'A', "World", 123456789Ul);
Console.WriteLine($"Type of tuple: {tuple.GetType()}");
Console.WriteLine(tuple);
Console.WriteLine($"output first element of tuple: {tuple.Item1}");

//Unpacking of tuple


(int num, string text1, char symbol, string text2, ulong bigNumber) = tuple;
Console.WriteLine($"unpacking: {number}, {symbol}");
var (tupleNumber, txt1, sym, txt2, bigNum) = tuple;
var (item1, item2, item3, _, _) = tuple;
Console.WriteLine($"compare of tuple 1 and tuple2: {tuple.CompareTo(tuple2)}");


//function

int[] number2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
string text = "Hello World";

(int max, int min, int sum, int firstLetter) ArrayFunction (int[] array, string str)
{
    if (array == null || array.Length == 0)
        throw new ArgumentException("array cannot be ampty");
    if (string.IsNullOrEmpty(str))
        throw new ArgumentException("stroke cannot be ampty");

    int maxValue = array[0];
    int minValue = array[0];
    int totalSum = 0;

    foreach (int number in array)
    {
        if (number > maxValue)
            maxValue = number;
        if (number < minValue)
            minValue = number;
        totalSum += number;
    }

    char firstChar = str[0];
    return (maxValue, minValue, totalSum, firstChar);
}

var result = ArrayFunction(number2, text);
Console.WriteLine($"max element = {result.max}");
Console.WriteLine($"min element = {result.min}");

//checked and Unchecked 

int maxInt = int.MaxValue;
unchecked
{
    int result1 = maxInt + 1;
    Console.WriteLine($"unchecked: {maxInt} + 1 = {result1}");
}
try
{
    checked
    {
        int result2 = maxInt + 1;
        Console.WriteLine($"checked: {maxInt} + 1 = {result2}");
    }
}
catch
{
    Console.WriteLine($"overflowing");
}