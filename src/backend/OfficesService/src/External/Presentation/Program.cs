using Presentation.Extensions;

namespace Presentation;

public static class Program
{
    public static async Task Main(string[] args)
    {
        await WebApplication
            .CreateBuilder(args)
            .ConfigureBuilder()
            .Build()
            .ConfigureApplication()
            .RunApplicationAsync();
    }
}