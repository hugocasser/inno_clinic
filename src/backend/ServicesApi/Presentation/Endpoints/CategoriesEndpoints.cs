using BLL.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common;

namespace Presentation.Endpoints;

public static class CategoriesEndpoints
{
    public static void MapCategoriesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/categories",
            async ([FromServices] ICategoriesService categoriesService, CancellationToken cancellationToken) =>
            {
                var result = await categoriesService.GetAllAsync(cancellationToken);

                return ResultsMapper.MapFormOperationResult(result);
            });
    }
}