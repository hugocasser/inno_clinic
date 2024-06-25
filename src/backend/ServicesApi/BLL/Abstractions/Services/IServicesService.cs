using BLL.Dtos.Requests;
using BLL.Dtos.Requests.CreateService;
using BLL.Dtos.Requests.PageSettings;
using BLL.Dtos.Requests.ServiceUpdate;
using BLL.Result;

namespace BLL.Abstractions.Services;

public interface IServicesService
{
    public Task<OperationResult> AddAsync(CreateServiceDto request, CancellationToken cancellationToken);
    public Task<OperationResult> RemoveAsync(Guid serviceId, CancellationToken cancellationToken);
    public Task<OperationResult> GetAllAsync(PageSettings pageSettings, CancellationToken cancellationToken = default);
    public Task<OperationResult> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<OperationResult> UpdateAsync(ServiceUpdateDto request, CancellationToken cancellationToken = default);
    public Task<OperationResult> ChangeStatusAsync(Guid id, bool status, CancellationToken cancellationToken = default);
}