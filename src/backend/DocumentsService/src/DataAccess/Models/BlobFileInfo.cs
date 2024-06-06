namespace DataAccess.Models;

public class BlobFileInfo
{
    public Guid Id { get; set; }
    public Guid FileId { get; set; }
    public DateOnly UploadedDate { get; set; }
    public DateOnly LastDownloadDate { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public int Size { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsDeletedFromBlobStorage { get; set; }
}