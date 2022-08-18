namespace Wuphf;

public static class ContentPageExtensions
{
    public static void Update(this ContentPage page)
    {
        page.Dispatcher.Dispatch(() =>
        {
            var c = page.BindingContext;
            page.BindingContext = null;
            page.BindingContext = c;
        });
    }
}