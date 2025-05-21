public class ThemeService
{
    public bool IsLightTheme { get; private set; } = false;

    public event Func<Task>? OnThemeChanged;

    public async Task ToggleThemeAsync()
    {
        IsLightTheme = !IsLightTheme;
        if (OnThemeChanged is not null)
            await OnThemeChanged.Invoke();
    }
}
