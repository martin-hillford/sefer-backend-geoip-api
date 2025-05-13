var app = Setup.CreateApp(args);

app.MapGet("/{ipAddress}", async (IMemoryCache cache, IServiceOptionsProvider provider, string ipAddress, [FromQuery] string apiKey, [FromQuery] bool store = false) =>
{
    // Get the options that are required to use the service
    var options = provider.GetServiceOptions();

    // check if the request has provided an api key
    if (string.IsNullOrEmpty(options.ApiKey)) return Results.Unauthorized();
    if (apiKey != options.ApiKey) return Results.Unauthorized();

    // check if the check contains the ipAddress
    var cached = cache.Get<Result>(ipAddress);
    if (cached != null) return Results.Json(cached);

    // lookup the ip address
    var resolver = new Resolver(options);
    var result = await resolver.Lookup(ipAddress);
    if (result == null) return Results.NotFound();

    // Check if the results needs to be stored
    if (store) await Storage.Create(options).SaveResultAsync(ipAddress, result);
    cache.Set(ipAddress, result, DateTimeOffset.UtcNow.AddHours(12));
    return Results.Json(result);
});

app.Run();