using BLL.Abstractions.Services;
using BLL.Dtos.Requests.CreateSpecialization;
using BLL.Dtos.Requests.EditSpecialization;
using BLL.Dtos.Requests.PageSettings;
using BLL.Dtos.Views;
using BLL.Resources;
using BLL.Result;
using DLL.Abstractions.Persistence.Repositories;

namespace BLL.Services;

public class SpecializationsService(ISpecializationsRepository specializationsRepository) : ISpecializationsService
{
    public async Task<OperationResult> CreateAsync(CreateSpecializationDto request, CancellationToken cancellationToken)
    {
        var specialization = request.MapToModel();
        
        var queryResult = await specializationsRepository.AddAsync(specialization);

        specializationsRepository.Commit();
        
        return queryResult !=1 
            ? ResultBuilder.UnexpectedError() 
            : ResultBuilder.Success(specialization.Id);
    }

    public async Task<OperationResult> ChangeStatusAsync(Guid specializationId, bool status,
        CancellationToken cancellationToken)
    {
        var specialization = await specializationsRepository.GetByIdAsync(specializationId);

        if (specialization is null)
        {
            return ResultBuilder.NotFound(RespounseMessages.SpecializationNotFound);
        }

        if (specialization.IsActive == status)
        {
            return ResultBuilder.BadRequest(RespounseMessages.SpecializationStatusNotChanged);
        }
        
        var queryResult = await specializationsRepository.UpdateStatusAsync(specializationId, status);

        if (queryResult != 1)
        {
            specializationsRepository.Rollback();
            
            return ResultBuilder.UnexpectedError();    
        }
        
        specializationsRepository.Commit();

        return ResultBuilder.Success();
    }

    public async Task<OperationResult> EditSpecializationAsync(EditSpecializationDto request,
        CancellationToken cancellationToken)
    {
        var isExists = await specializationsRepository.IsExistsAsync(request.Id);
        
        if (!isExists)
        {
            return ResultBuilder.NotFound(RespounseMessages.SpecializationNotFound);
        }
        
        var specialization = request.MapToModel();
        
        var queryResult = await specializationsRepository.UpdateAsync(specialization, specialization.IsActive);
        
        if (queryResult != 1)
        {
            specializationsRepository.Rollback();
            
            return ResultBuilder.UnexpectedError();
        }
        
        specializationsRepository.Commit();
        
        return ResultBuilder.Success();
    }

    public async Task<OperationResult> GetByIdAsync(Guid specializationId, CancellationToken cancellationToken)
    {
        var specialization = await specializationsRepository.GetByIdAsync(specializationId);

        specializationsRepository.Commit();
        
        return specialization is not null 
            ? ResultBuilder.Success(SpecializationViewDto.MapFromModel(specialization))
            : ResultBuilder.NotFound(RespounseMessages.SpecializationNotFound);
    }

    public async Task<OperationResult> GetAllAsync(PageSettings pageSettings, CancellationToken cancellationToken)
    {
        var take = pageSettings.PageSize;
        var skip = (pageSettings.PageNumber - 1) * take;
        
        var queryResult = await specializationsRepository
            .GetAllAsync(take, skip);
        
        specializationsRepository.Commit();
        
        return ResultBuilder
            .Success(queryResult
                .Select(SpecializationListItemViewDto.MapFromModel)
                .ToList());
    }
}