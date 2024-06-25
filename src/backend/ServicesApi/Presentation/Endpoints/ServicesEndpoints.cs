using BLL.Abstractions.Services;
using BLL.Dtos.Requests;
using BLL.Dtos.Requests.CreateService;
using BLL.Dtos.Requests.PageSettings;
using BLL.Dtos.Requests.ServiceUpdate;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common;

namespace Presentation.Endpoints;

public static class ServicesEndpoints
{
    public static IEndpointRouteBuilder MapServicesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/services/{pageNumber:int}/{pageSize:int}", 
            async ([FromRoute]int pageNumber, [FromRoute]int pageSize, [FromServices]IServicesService servicesService,
                CancellationToken cancellationToken) =>
        {
            var result = await servicesService.GetAllAsync(new PageSettings(pageSize, pageNumber),cancellationToken);
            
            return ResultsMapper.MapFormOperationResult(result);
        });
        
        app.MapGet("/api/services/{id:guid}", 
            async ([FromRoute]Guid id, [FromServices]IServicesService servicesService,
                CancellationToken cancellationToken) =>
        {
            var result = await servicesService.GetByIdAsync(id, cancellationToken);
            
            return ResultsMapper.MapFormOperationResult(result);
        });
        
        app.MapPut("/api/services", 
            async ([FromBody]ServiceUpdateDto request, [FromServices]IServicesService servicesService,
                CancellationToken cancellationToken) =>
        {
            var result = await servicesService.UpdateAsync(request, cancellationToken);
            
            return ResultsMapper.MapFormOperationResult(result);
        });
        
        app.MapPost("/api/services/", 
            async ([FromBody]CreateServiceDto request, [FromServices]IServicesService servicesService,
                CancellationToken cancellationToken) =>
        {
            var result = await servicesService.AddAsync(request, cancellationToken);
            
            return ResultsMapper.MapFormOperationResult(result);
        });
        
        app.MapDelete("/api/services/{id:guid}", 
            async ([FromRoute]Guid id, [FromServices]IServicesService servicesService,
                CancellationToken cancellationToken) =>
        {
            var result = await servicesService.RemoveAsync(id, cancellationToken);
            
            return ResultsMapper.MapFormOperationResult(result);
        });
        
        app.MapGet("/api/services/status/{id:guid}/{status:bool}", 
            async ([FromRoute]Guid id, [FromRoute]bool status, [FromServices]IServicesService servicesService,
                CancellationToken cancellationToken) =>
        {
            var result = await servicesService.ChangeStatusAsync(id, status, cancellationToken);
            
            return ResultsMapper.MapFormOperationResult(result);
        });
        
        return app;
    }
}