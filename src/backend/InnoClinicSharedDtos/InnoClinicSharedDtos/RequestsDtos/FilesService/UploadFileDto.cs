using Microsoft.AspNetCore.Http;

namespace InnoClinicSharedDtos.RequestsDtos.FilesService;

public record UploadFileDto(IFormFile File, string FileName);