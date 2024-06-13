using Microsoft.Extensions.Logging;
using Presentation.Extensions;

namespace Presentation;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        return MauiApp
            .CreateBuilder()
            .ConfigureAppBuilder()
            .Build();
    }
}