using BLL.Abstractions.Services;
using BLL.Dtos.Requests;
using BLL.Dtos.Requests.PageSettings;
using BLL.Dtos.Requests.ServiceUpdate;
using BLL.Dtos.Views;
using BLL.Resources;
using BLL.Result;
using Dapper;
using DLL.Abstractions.Persistence.Repositories;
using DLL.Models;
using DLL.Persistence.Repositories;

namespace BLL.Services;

public class ServicesService(ISpecializationsRepository specializationsRepository, IServicesRepository servicesRepository) : IServicesService
{
    public async Task<OperationResult> AddAsync(CreateServiceDto request, CancellationToken cancellationToken)
    {
        using var transaction = servicesRepository.CurrentTransaction();
        
        var isSpecializationExist = await specializationsRepository.IsExistsAsync(request.SpecializationId);
        
        if (!isSpecializationExist)
        {
            return ResultBuilder.BadRequest(RespounseMessages.SpecializationNotFound);
        }
        
        var isCategoryExist = await transaction.Connection!.IsExistsAsync<Category>(request.CategoryId);
        
        if (!isCategoryExist)
        {
            return ResultBuilder.BadRequest(RespounseMessages.CategoryNotFound);
        }
        
        var id = Guid.NewGuid();
        
        var addQuery = $"INSERT INTO Services (id, name, specialization_id, category_id, status, price)" +
            $" VALUES ({id}, {request.Name}, {request.SpecializationId}, {request.CategoryId}, {request.IsActive}, {request.Price})";
        
        var result = await transaction.Connection!.ExecuteAsync(addQuery, transaction: transaction);
        
        if (result != 1)
        {
            return ResultBuilder.UnexpectedError();
        }
        servicesRepository.SaveChanges();

        return ResultBuilder.Success(id);
    }

    public async Task<OperationResult> RemoveAsync(Guid serviceId, CancellationToken cancellationToken)
    {
        using var transaction = servicesRepository.CurrentTransaction();
        var isExists = await servicesRepository.CurrentTransaction().Connection!.IsExistsAsync<Service>(serviceId, transaction: servicesRepository.CurrentTransaction());
        
        if (!isExists)
        {
            return ResultBuilder.NotFound(RespounseMessages.ServiceNotFound);
        }
        
        await transaction.Connection!.DeleteAsync<Service>(serviceId, transaction);
        
        servicesRepository.SaveChanges();
        
        return ResultBuilder.Success();
    }

    public async Task<OperationResult> GetAllAsync(PageSettings pageSettings, CancellationToken cancellationToken = default)
    {
     
        using var transaction = servicesRepository.CurrentTransaction();
        var take = pageSettings.PageSize;
        var skip = (pageSettings.PageNumber - 1) * pageSettings.PageSize;
        
        var query = $"SELECT Id, Name, Status, Price FROM Services WHERE IsDeleted = false LIMIT {take} OFFSET {skip}";
        var result = await servicesRepository.CurrentTransaction()
            .Connection!.QueryAsync<Service>(query, transaction: transaction);
        
        var mappedResult = result.ToList().Select(ServiceListItemViewDto.FromModel).ToList();
        
        var operationResult = ResultBuilder.Success(mappedResult);
        
        servicesRepository.SaveChanges();
        
        return operationResult;
    }

    public async Task<OperationResult> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var transaction = servicesRepository.CurrentTransaction();
        
        var service = await servicesRepository.CurrentTransaction()
            .Connection!.GetByIdAsync<Service>(id, transaction);

        if (service is null)
        {
            return ResultBuilder.NotFound(RespounseMessages.ServiceNotFound);
        }
        
        servicesRepository.SaveChanges();

        return ResultBuilder.Success(ServiceViewDto.FromModel(service));
    }

    public async Task<OperationResult> UpdateAsync(ServiceUpdateDto request, CancellationToken cancellationToken = default)
    {
        using var transaction = servicesRepository.CurrentTransaction();
        
        var service = await servicesRepository.CurrentTransaction()
            .Connection!.GetByIdAsync<Service>(request.Id, transaction);
        
        if (service is null)
        {
            return ResultBuilder.NotFound(RespounseMessages.ServiceNotFound);
        }
        
        var isSpecializationExist = await specializationsRepository.IsExistsAsync(request.SpecializationId);
        
        if (!isSpecializationExist)
        {
            return ResultBuilder.BadRequest(RespounseMessages.SpecializationNotFound);
        }
        
        var isCategoryExist = await transaction.Connection!.IsExistsAsync<Category>(request.CategoryId);

        if (!isCategoryExist)
        {
            return ResultBuilder.BadRequest(RespounseMessages.CategoryNotFound);
        }

        service.Name = request.Name;
        service.IsActive = request.IsActive;
        service.Price = request.Price;
        service.SpecializationId = request.SpecializationId;
        service.CategoryId = request.CategoryId;
        
        var query = $"UPDATE Services SET name = {request.Name}, specialization_id = {request.SpecializationId}, " +
            $"category_id = {request.CategoryId}, status = {request.IsActive}, price = {request.Price} WHERE id = {request.Id}";
        
        var result = await transaction.Connection!.ExecuteAsync(query, transaction: transaction);

        if (result != 1)
        {
            transaction.Rollback();

            return ResultBuilder.UnexpectedError();
        }
        
        servicesRepository.SaveChanges(); 
        
        return ResultBuilder.Success();
    }

    public async Task<OperationResult> ChangeStatusAsync(Guid id, bool status, CancellationToken cancellationToken = default)
    {
        var transaction = servicesRepository.CurrentTransaction();
        
        var service = await transaction.Connection!.GetByIdAsync<Service>(id, transaction);
        
        if (service is null)
        {
            return ResultBuilder.NotFound(RespounseMessages.ServiceNotFound);
        }
        
        if (service.IsActive == status)
        {
            return ResultBuilder.BadRequest(RespounseMessages.ServiceStatusNotChanged);
        }
        
        var query = $"UPDATE Services SET status = {status} WHERE id = {id}";

        return await transaction.Connection!.ExecuteAsync(query, transaction: transaction) == 1
            ? ResultBuilder.Success()
            : ResultBuilder.UnexpectedError();
    }
}