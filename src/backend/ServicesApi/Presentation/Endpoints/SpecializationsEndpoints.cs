using BLL.Abstractions.Services;
using BLL.Dtos.Requests.PageSettings;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common;

namespace Presentation.Endpoints;

public static class SpecializationsEndpoints
{
    public static IEndpointRouteBuilder MapSpecializationsEndpoints(this IEndpointRouteBuilder app)
    {
        
        app.MapGet("/api/specializations{page:int}/{pageSize:int}",
            async ([FromServices] ISpecializationsService specializationsService, [FromRoute]int page, [FromRoute]int pageSize,
                CancellationToken cancellationToken) =>
            {
                var result = await specializationsService.GetAllAsync(new PageSettings(page, pageSize), cancellationToken);
                
                return ResultsMapper.MapFormOperationResult(result);
            });
        
        app.MapGet("/api/specializations/{id:guid}", 
            async ([FromRoute]Guid id, [FromServices] ISpecializationsService specializationsService,
                CancellationToken cancellationToken) =>
            {
                var result = await specializationsService.GetByIdAsync(id, cancellationToken);
                
                return ResultsMapper.MapFormOperationResult(result);
            });
        return app;
    }
}