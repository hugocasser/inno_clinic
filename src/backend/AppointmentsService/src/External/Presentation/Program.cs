using Presentation.Extensions;

public class Program
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