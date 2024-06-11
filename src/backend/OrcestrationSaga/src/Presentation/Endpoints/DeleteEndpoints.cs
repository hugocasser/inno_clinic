using Application.Abstractions.Services.Saga;
using Application.Transactions.DeleteDoctorTransaction;
using Application.Transactions.DeletePatientTransaction;
using Presentation.Common;

namespace Presentation.Endpoints;

public static class DeleteEndpoints
{
    public static WebApplication MapDeleteEndpoints(this WebApplication app)
    {
        app.MapDelete("/api/patients/{patientId}", 
            async (Guid patientId, ITransactionsHandlerService orchestrator) =>
            {
                var transaction = new DeletePatientTransaction
                {
                    AccountId = patientId
                };

                var result = await orchestrator.StartExecuteAsync(transaction);

                return HttpResultBuilder.BuildResult(result);
            });
        
        app.MapDelete("/api/doctors/{doctorId}",
            async (Guid doctorId, ITransactionsHandlerService orchestrator) =>
            {
                var transaction = new DeleteDoctorTransaction
                {
                    AccountId = doctorId
                };
                
                var result = await orchestrator.StartExecuteAsync(transaction);
                
                return HttpResultBuilder.BuildResult(result);
            });
        
        return app;
    }
}