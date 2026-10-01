using Microsoft.JSInterop;
using MudBlazor;
using System.Security.Cryptography;

namespace blazor_frontend.Services;

public enum ThemeMode
{
    Light,
    Dark
}

public sealed class ThemeState : IAsyncDisposable
{
    public const string StorageKey = "themeMode";

    private readonly IJSRuntime _js;
    private readonly IWebHostEnvironment _env;
    private readonly MudTheme _theme = ThemeFactory.CreateTheme();

    private IJSObjectReference? _module;
    private string? _themeScriptVersion;

    public ThemeState(IJSRuntime js, IWebHostEnvironment env)
    {
        _js = js;
        _env = env;
    }

    public ThemeMode Mode { get; private set; } = ThemeMode.Light;

    public bool IsDarkMode => Mode == ThemeMode.Dark;

    public MudTheme Theme => _theme;

    public event Action? OnChange;

    public async Task InitializeAsync()
    {
        try
        {
            // Cache-bust the module by content hash so a redeploy is picked up
            // instead of the browser's cached copy of theme.js.
            var version = _themeScriptVersion ??= GetThemeScriptVersion();
            _module = await _js.InvokeAsync<IJSObjectReference>("import", $"./js/theme.js?v={version}");
            var mode = await _module.InvokeAsync<string>("getMode");
            Mode = Parse(mode);
            NotifyStateChanged();
        }
        catch (JSDisconnectedException)
        {
        }
    }

    public async Task ToggleThemeAsync()
    {
        Mode = IsDarkMode ? ThemeMode.Light : ThemeMode.Dark;

        if (_module is not null)
        {
            try
            {
                await _module.InvokeVoidAsync("setMode", ToStorageValue(Mode));
            }
            catch (JSDisconnectedException)
            {
            }
        }

        NotifyStateChanged();
    }

    private string GetThemeScriptVersion()
    {
        try
        {
            var path = Path.Combine(_env.WebRootPath, "js", "theme.js");
            if (File.Exists(path))
            {
                return Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(path)))[..8];
            }
        }
        catch
        {
        }

        return "1";
    }

    private static string ToStorageValue(ThemeMode mode) => mode == ThemeMode.Dark ? "dark" : "light";

    private static ThemeMode Parse(string? value) =>
        string.Equals(value, "dark", StringComparison.OrdinalIgnoreCase) ? ThemeMode.Dark : ThemeMode.Light;

    private void NotifyStateChanged() => OnChange?.Invoke();

    public async ValueTask DisposeAsync()
    {
        if (_module is not null)
        {
            try
            {
                await _module.DisposeAsync();
            }
            catch (JSDisconnectedException)
            {
            }
            catch (ObjectDisposedException)
            {
            }
        }
    }
}
