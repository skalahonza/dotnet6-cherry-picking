namespace Demo1.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Turns the input into asterisks
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string Asteriskify(this string input) =>
        new(Enumerable.Repeat('*', input.Length).ToArray());

}
