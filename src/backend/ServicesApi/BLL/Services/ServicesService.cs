using BLL.Abstractions.Services;
using BLL.Dtos.Requests;
using BLL.Dtos.Requests.PageSettings;
using BLL.Dtos.Requests.ServiceUpdate;
using BLL.Dtos.Views;
using BLL.Resources;
using BLL.Result;
using DLL.Abstractions.Persistence.Repositories;

namespace BLL.Services;

public class ServicesService
    (ISpecializationsRepository specializationsRepository,
        IServicesRepository servicesRepository,
        ICategoriesRepository categoriesRepository) : IServicesService
{
    public async Task<OperationResult> AddAsync(CreateServiceDto request, CancellationToken cancellationToken)
    {
        var isSpecializationExist = await specializationsRepository.IsExistsAsync(request.SpecializationId);
        
        if (!isSpecializationExist)
        {
            return ResultBuilder.BadRequest(RespounseMessages.SpecializationNotFound);
        }
        
        var isCategoryExist = await categoriesRepository.IsExistsAsync(request.CategoryId);
        
        if (!isCategoryExist)
        {
            return ResultBuilder.BadRequest(RespounseMessages.CategoryNotFound);
        }

        var model = request.MapToModel();
        var result = await servicesRepository.AddAsync(model);
        
        if (result != 1)
        {
            return ResultBuilder.UnexpectedError();
        }
        servicesRepository.Commit();

        return ResultBuilder.Success(model.Id);
    }

    public async Task<OperationResult> RemoveAsync(Guid serviceId, CancellationToken cancellationToken)
    {
        var isExists = await servicesRepository.IsExistsAsync(serviceId);
        
        if (!isExists)
        {
            return ResultBuilder.NotFound(RespounseMessages.ServiceNotFound);
        }
        
        var result = await servicesRepository.DeleteAsync(serviceId);
        
        if (result != 1)
        {
            servicesRepository.Rollback();
            
            return ResultBuilder.UnexpectedError();
        }
        
        servicesRepository.Commit();
        
        return ResultBuilder.Success();
    }

    public async Task<OperationResult> GetAllAsync(PageSettings pageSettings, CancellationToken cancellationToken = default)
    {
        var take = pageSettings.PageSize;
        var skip = (pageSettings.PageNumber - 1) * pageSettings.PageSize;
        
        var result = await servicesRepository.GetAllAsync(take, skip);
        
        var mappedResult = result.ToList().Select(ServiceListItemViewDto.FromModel).ToList();
        
        var operationResult = ResultBuilder.Success(mappedResult);
        
        servicesRepository.Commit();
        
        return operationResult;
    }

    public async Task<OperationResult> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var service = await servicesRepository.GetByIdAsync(id);

        if (service is null)
        {
            return ResultBuilder.NotFound(RespounseMessages.ServiceNotFound);
        }
        
        servicesRepository.Commit();

        return ResultBuilder.Success(ServiceViewDto.FromModel(service));
    }

    public async Task<OperationResult> UpdateAsync(ServiceUpdateDto request, CancellationToken cancellationToken = default)
    {
        var service = await servicesRepository.GetByIdAsync(request.Id);
        
        if (service is null)
        {
            return ResultBuilder.NotFound(RespounseMessages.ServiceNotFound);
        }
        
        var isSpecializationExist = await specializationsRepository.IsExistsAsync(request.SpecializationId);
        
        if (!isSpecializationExist)
        {
            return ResultBuilder.BadRequest(RespounseMessages.SpecializationNotFound);
        }
        
        var isCategoryExist = await categoriesRepository.IsExistsAsync(request.CategoryId);

        if (!isCategoryExist)
        {
            return ResultBuilder.BadRequest(RespounseMessages.CategoryNotFound);
        }

        service.Name = request.Name;
        service.IsActive = request.IsActive;
        service.Price = request.Price;
        service.SpecializationId = request.SpecializationId;
        service.CategoryId = request.CategoryId;
        
        var result = await servicesRepository.UpdateAsync(service);

        if (result != 1)
        {
            servicesRepository.Rollback();

            return ResultBuilder.UnexpectedError();
        }
        
        servicesRepository.Commit(); 
        
        return ResultBuilder.Success();
    }

    public async Task<OperationResult> ChangeStatusAsync(Guid id, bool status, CancellationToken cancellationToken = default)
    {
        var service = await servicesRepository.GetByIdAsync(id);
        
        if (service is null)
        {
            return ResultBuilder.NotFound(RespounseMessages.ServiceNotFound);
        }
        
        if (service.IsActive == status)
        {
            return ResultBuilder.BadRequest(RespounseMessages.ServiceStatusNotChanged);
        }

        return await servicesRepository.UpdateStatusAsync(id, status) == 1
            ? ResultBuilder.Success()
            : ResultBuilder.UnexpectedError();
    }
}