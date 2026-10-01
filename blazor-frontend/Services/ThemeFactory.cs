using MudBlazor;

namespace blazor_frontend.Services;

public static class ThemeFactory
{
    public static MudTheme CreateTheme()
    {
        return new MudTheme
        {
            PaletteLight = new PaletteLight
            {
                Primary = "#2d5016",
                PrimaryContrastText = "#FFFFFF",
                Secondary = "#dc004e",
                SecondaryContrastText = "#FFFFFF",
                Background = "#FFFFFF",
                Surface = "#FFFFFF",
                AppbarBackground = "#FFFFFF",
                DrawerBackground = "#FFFFFF",
                TextPrimary = "rgba(0,0,0,0.87)",
                TextSecondary = "rgba(0,0,0,0.6)",
                TextDisabled = "rgba(0,0,0,0.38)",
                Divider = "rgba(0,0,0,0.12)",
            },
            PaletteDark = new PaletteDark
            {
                Primary = "#2d5016",
                PrimaryContrastText = "#FFFFFF",
                Secondary = "#dc004e",
                SecondaryContrastText = "#FFFFFF",
                Background = "#121212",
                Surface = "#121212",
                AppbarBackground = "#121212",
                DrawerBackground = "#121212",
                TextPrimary = "#FFFFFF",
                TextSecondary = "rgba(255,255,255,0.7)",
                TextDisabled = "rgba(255,255,255,0.5)",
                Divider = "rgba(255,255,255,0.12)",
            },
            Typography = new Typography
            {
                Default = new DefaultTypography
                {
                    FontFamily = new[] { "Roboto", "Helvetica", "Arial", "sans-serif" }
                },
                H1 = new H1Typography { FontSize = "2.5rem", FontWeight = "600" },
                H2 = new H2Typography { FontSize = "2rem", FontWeight = "500" },
                H3 = new H3Typography { FontSize = "1.75rem", FontWeight = "500" },
            }
        };
    }
}
