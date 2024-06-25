using BLL.Dtos.Requests.CreateSpecialization;
using BLL.Dtos.Requests.EditSpecialization;
using BLL.Dtos.Requests.PageSettings;
using BLL.Result;

namespace BLL.Abstractions.Services;

public interface ISpecializationsService
{
    public Task<OperationResult> CreateAsync(CreateSpecializationDto request, CancellationToken cancellationToken);
    public Task<OperationResult> ChangeStatusAsync(Guid specializationId, bool status, CancellationToken cancellationToken);
    public Task<OperationResult> EditSpecializationAsync(EditSpecializationDto request, CancellationToken cancellationToken);
    public Task<OperationResult> GetByIdAsync(Guid specializationId, CancellationToken cancellationToken);
    public Task<OperationResult> GetAllAsync(PageSettings pageSettings, CancellationToken cancellationToken = default);
}