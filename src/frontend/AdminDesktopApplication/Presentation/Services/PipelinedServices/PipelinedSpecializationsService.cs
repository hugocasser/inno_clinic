using Application.Abstractions;
using Application.Abstractions.Services;
using Application.Dtos;
using Application.Results;
using Presentation.Abstractions.Services;
using Presentation.Abstractions.Services.PipelinedService;

namespace Presentation.Services.PipelinedServices;

public class PipelinedSpecializationsService(
    ISpecializationsService specializationsService,
    ICredentialsService credentialsService): IPipelinedSpecializationsService
{
    public async Task<IResult> GetAllSpecializationsAsync()
    {
        var result = new Result();
        var requestResult = await specializationsService.GetAllSpecializationsAsync();

        if (!requestResult.IsSuccess)
        {
            var response = requestResult.GetResultData<string>();

            if (response == TextResponses.Unauthorized)
            {
                var refreshResult = await credentialsService.TryRefreshTokenAsync();

                if (!refreshResult.IsSuccess)
                {
                    result.SetResultData(TextResponses.Unauthorized);

                    return result;
                }

                requestResult = await specializationsService.GetAllSpecializationsAsync();
            }
        
            if (!requestResult.IsSuccess)
            {
                result.SetResultData(TextResponses.SomethingWentWrong);
                
                return result;
            }
        }

        var dto = requestResult.GetResultData<List<SpecializationViewDto>>();
    
        if (dto is not null)
        {
            result.SetResultData(dto);
            return result;
        }

        result.SetResultData(TextResponses.SomethingWentWrong);
        
        return result;
    }

    public async Task<IResult> GetSpecializationNameByIdAsync(Guid id)
    {
        var result = new Result();
        var requestResult = await specializationsService.GetSpecializationNameByIdAsync(id);

        if (!requestResult.IsSuccess)
        {
            var response = requestResult.GetResultData<string>();

            if (response == TextResponses.Unauthorized)
            {
                var refreshResult = await credentialsService.TryRefreshTokenAsync();

                if (!refreshResult.IsSuccess)
                {
                    result.SetResultData(TextResponses.Unauthorized);

                    return result;
                }

                requestResult = await specializationsService.GetSpecializationNameByIdAsync(id);
            }
        
            if (!requestResult.IsSuccess)
            {
                result.SetResultData(TextResponses.SomethingWentWrong);
                
                return result;
            }
        }

        var dto = requestResult.GetResultData<SpecializationViewDto>();
    
        if (dto is not null)
        {
            result.SetResultData(dto.Name);
            return result;
        }

        result.SetResultData(TextResponses.SomethingWentWrong);
        
        return result;
    }
}