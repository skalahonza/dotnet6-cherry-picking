using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

var time = TimeOnly.FromDateTime(DateTime.Now);
var date = DateOnly.FromDateTime(DateTime.UtcNow);

Console.WriteLine(time.AddHours(26));
Console.WriteLine(date.AddDays(3));


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => new HelloResponse(TimeOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now)));

app.Run();


public record HelloResponse
(
TimeOnly Time,
DateOnly Date
);











public class HelloResponse2
{
    public HelloResponse2()
    {

    }

    public HelloResponse2(TimeOnly time, DateOnly date)
    {
        Time = time;
        Date = date;
    }

    [JsonConverter(typeof(TimeOnlyJsonConverter))]
    public TimeOnly Time { get; set; }
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly Date { get; set; }
}



public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) =>
            DateOnly.ParseExact(reader.GetString(),
                "yyyy-MM-dd", CultureInfo.InvariantCulture);

    public override void Write(
        Utf8JsonWriter writer,
        DateOnly dateTimeValue,
        JsonSerializerOptions options) =>
            writer.WriteStringValue(dateTimeValue.ToString(
                "yyyy-MM-dd", CultureInfo.InvariantCulture));
}

public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
    public override TimeOnly Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) =>
            TimeOnly.ParseExact(reader.GetString(),
                "HH:mm:ss", CultureInfo.InvariantCulture);

    public override void Write(
        Utf8JsonWriter writer,
        TimeOnly dateTimeValue,
        JsonSerializerOptions options) =>
            writer.WriteStringValue(dateTimeValue.ToString(
                "HH:mm:ss", CultureInfo.InvariantCulture));
}