using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using System;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", async (int? seconds) =>
{
    seconds ??= 1;
    await Task.Delay(TimeSpan.FromSeconds(seconds.Value));
    return Results.Ok($"It only took {seconds} seconds.");
});

app.Run();