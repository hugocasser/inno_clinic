using Application.Abstractions.Services.Saga;
using Application.Dtos.Requests;
using Application.Transactions.UpdateDoctorTransaction;
using Application.Transactions.UpdatePatientTransaction;
using Presentation.Common;

namespace Presentation.Endpoints;

public static class UpdateEndpoints
{
    public static WebApplication MapUpdateEndpoints(this WebApplication app)
    {
        app.MapPut("/api/patients", async (UpdatePatientsUnitedDto request, ITransactionsHandlerService orchestrator) =>
        {
            var transaction = UpdatePatientTransaction
                .MapFromRequest(request);

            var result = await orchestrator.StartExecuteAsync(transaction);

            return HttpResultBuilder.BuildResult(result);
        });
        
        app.MapPut("/api/doctors", async (UpdateDoctorsUnitedDto request, ITransactionsHandlerService orchestrator) =>
        {
            var transaction = UpdateDoctorTransaction
                .MapFromRequest(request);
            
            var result = await orchestrator.StartExecuteAsync(transaction);
            
            return HttpResultBuilder.BuildResult(result);
        });
        
        return app;
    }
}