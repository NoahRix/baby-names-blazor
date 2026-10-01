using blazor_frontend.Services;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor(options =>
{
    options.DetailedErrors = true;
});
builder.Services.AddHttpClient();
builder.Services.AddScoped(sp => {
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var client = httpClientFactory.CreateClient();

    // When hosted behind the nginx reverse proxy, the browser-facing Host is not
    // reachable from inside the container, so the base URL is supplied via configuration.
    // Falls back to the current request's origin for direct (non-proxied) hosting.
    var baseUrl = sp.GetRequiredService<IConfiguration>()["HttpClientBaseUrl"];
    if (!string.IsNullOrWhiteSpace(baseUrl))
    {
        client.BaseAddress = new Uri(baseUrl);
    }
    else
    {
        var request = sp.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Request;
        if (request != null)
        {
            client.BaseAddress = new Uri($"{request.Scheme}://{request.Host}");
        }
    }

    return client;
});
builder.Services.AddScoped<AppState>();
builder.Services.AddScoped<ThemeState>();
builder.Services.AddMudServices();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
