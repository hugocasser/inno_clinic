using Presentation.Extensions;

namespace Presentation;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var app = MauiApp
            .CreateBuilder()
            .ConfigureAppBuilder()
            .Build();
        
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
        {
            
#if MACCATALYST
            var size = new CoreGraphics.CGSize(550, 800);

            if (handler.PlatformView.WindowScene is not { SizeRestrictions: not null })
            {
                return;
            }

            handler.PlatformView.WindowScene.SizeRestrictions.MinimumSize = size;
            handler.PlatformView.WindowScene.SizeRestrictions.MaximumSize = size;
#endif
        });
        
        return app;
    }
}