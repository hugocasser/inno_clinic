namespace DataAccess.Dtos;

public record FileResponse(Stream? Stream, string ContentType) : IAsyncDisposable
{
    public async ValueTask DisposeAsync()
    {
        if (Stream != null) await Stream.DisposeAsync();
    }
}