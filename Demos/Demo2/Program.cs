var names = new[] { "Alice", "Bob", "Cletus", "Daria", "Alice" };
var surnames = new[] { "Doe", "Novak", "Nguyen", "Smith", "Smith" };
var ages = new[] { 10, 20, 30, 40 };







































// 3 way zipping
IEnumerable<(string Name, string Surname, int Age)> zipped = names.Zip(surnames, ages);
var persons = zipped.Select(x => new Person(x.Name, x.Surname, x.Age)).ToList();



































// Chunk
var batches = persons.Chunk(2);

































// TryGetNonEnumeratedCount
var numbers = Enumerable.Range(1, 100);
var primes = numbers.Where(IsPrime);




































var count = numbers.Count();
count = numbers.Count();

if(numbers.TryGetNonEnumeratedCount(out count))
{
    Console.WriteLine(count);
}
else
{
    Console.WriteLine("numbers, no luck");
}

// lazy
if (primes.TryGetNonEnumeratedCount(out count))
{
    Console.WriteLine(count);
}
else
{
    Console.WriteLine("primes, no luck");
}

// eager
primes = primes.ToList();
if (primes.TryGetNonEnumeratedCount(out count))
{
    Console.WriteLine(count);
}
else
{
    Console.WriteLine("primes, no luck");
}








































// By
// max age
var maxAge = persons.Max(x => x.Age);
var oldestPerson = persons.MaxBy(x => x.Age);

// distinct
var uniqueNames = persons.DistinctBy(x => x.Name);

// and many more





































// slicing with take
// typical paging
var page = persons.Skip(2).Take(2).ToList();


// it is easier now
var page2 = persons.Take(2..4).ToList();
Console.ReadKey();






static bool IsPrime(int number)
{
    for (int divider = 2; divider < Math.Sqrt(number); divider++)
    {
        if(number % divider == 0) 
            return true;
    }

    return false;
}

public record Person(string Name, string Surname, int Age);