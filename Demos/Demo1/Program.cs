
// DEMO 1

// file scoped namespaces --> StringExtensions

// global using statements
Console.WriteLine("Hello, World!");
Console.WriteLine("Hello, World!".Asteriskify());
































// constant string interpolations
const string API_PREFIX = "api";
const string LOGIN = "login";
const string tmp = $"{API_PREFIX}/{LOGIN}";





























// better type inference for lambdas
var hello = () => "Hello";
var isNull = false;
Func<string?> hello2 = () => isNull ? null : "Hello";
// var hello3 = () => null;
























// record structs
var person = new Person("Jan");
// cannot do this: person.Name = "Jan2";

var person2 = new PersonStruct("Jan");
person2.Name = "Now we can.";

public record Person(string Name);
public record struct PersonStruct(string Name);




















/// <summary>
/// Old way of doing this
/// </summary>
public class PersonClass
{
    public PersonClass(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public override bool Equals(object? obj)
    {
        return obj is PersonClass @class &&
               Name == @class.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name);
    }
}

















// still preview
//[AttributeUsage(AttributeTargets.Class)]
//public class GenericAttribute<T> : Attribute
//{
//    public T Data { get; set; }
//}











// old way
[AttributeUsage(AttributeTargets.Class)]
public class NotSoGenericAttribute : Attribute
{
    public object Data { get; set; }
    public Type Type { get; set; }
}


[NotSoGeneric(Data = "test", Type = typeof(string))]
public class Foo
{

}