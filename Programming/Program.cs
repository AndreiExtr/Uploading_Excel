using System;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia;

namespace Programming
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var client = new HttpClient();
            var response = await client.GetStringAsync("https://andreiextr.github.io/Uploading_Excel/");

            Console.WriteLine(response);
        }

        
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();
    }
}
