using Application.Abstractions;
using Application.Abstractions.Services;
using Application.Dtos;
using Application.Results;
using Presentation.Abstractions.Services;
using Presentation.Abstractions.Services.PipelinedService;

namespace Presentation.Services.PipelinedServices;

public class PipelinedStatusesService(
    IStatusesService statusesService,
    ICredentialsService credentialsService) : IPipelinedStatusesService
{
    public async Task<IResult> GetStatusNameByIdAsync(Guid id)
    {
        var result = new Result();
        var requestResult = await statusesService.GetStatusNameByIdAsync(id);

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

                requestResult = await statusesService.GetStatusNameByIdAsync(id);
            }
        
            if (!requestResult.IsSuccess)
            {
                result.SetResultData(TextResponses.SomethingWentWrong);
                
                return result;
            }
        }

        var dto = requestResult.GetResultData<StatusViewDto>();
    
        if (dto is not null)
        {
            result.SetResultData(dto.Name);
            return result;
        }

        result.SetResultData(TextResponses.SomethingWentWrong);
        
        return result;
    }

    public async Task<IResult> GetAllStatusesAsync()
    {
        var result = new Result();
        var requestResult = await statusesService.GetAllStatusesAsync();

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

                requestResult = await statusesService.GetAllStatusesAsync();
            }
        
            if (!requestResult.IsSuccess)
            {
                result.SetResultData(TextResponses.SomethingWentWrong);
                
                return result;
            }
        }

        var dto = requestResult.GetResultData<List<StatusViewDto>>();
    
        if (dto is not null)
        {
            result.SetResultData(dto);
            return result;
        }

        result.SetResultData(TextResponses.SomethingWentWrong);
        
        return result;
    }
}