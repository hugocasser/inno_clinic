using BLL.Result;

namespace Presentation.Common;

public static class ResultsMapper
{
    public static IResult MapFormOperationResult(OperationResult operationResult)
    {
        if (operationResult.IsSuccess)
        {
            if (operationResult.Message is not null)
            {
                Results.Ok(operationResult.Message);
            }

            return Results.NoContent();
        }

        return operationResult.StatusCode switch
        {
            400 => Results.BadRequest(operationResult.Message),
            404 => Results.NotFound(operationResult.Message),
            _ => Results.BadRequest(operationResult.Message)
        };
    }
}