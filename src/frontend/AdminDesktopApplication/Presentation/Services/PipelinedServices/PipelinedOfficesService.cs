using Application.Abstractions;
using Application.Abstractions.Services;
using Application.Dtos;
using Application.Results;
using Presentation.Abstractions.Services;
using Presentation.Abstractions.Services.PipelinedService;

namespace Presentation.Services.PipelinedServices;

public class PipelinedOfficesService(
    ICredentialsService credentialsService,
    IOfficesService officesService) : IPipelinedOfficesService
{
    public async Task<IResult> GetAllAsync()
    {
        var result = new Result();
        var requestResult = await officesService.GetAllAsync();

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

                requestResult = await officesService.GetAllAsync();
            }
        
            if (!requestResult.IsSuccess)
            {
                result.SetResultData(TextResponses.SomethingWentWrong);
                
                return result;
            }
        }

        var dto = requestResult.GetResultData<List<OfficeViewDto>>();
    
        if (dto is not null)
        {
            result.SetResultData(dto);
            return result;
        }

        result.SetResultData(TextResponses.SomethingWentWrong);
        
        return result;
    }
}