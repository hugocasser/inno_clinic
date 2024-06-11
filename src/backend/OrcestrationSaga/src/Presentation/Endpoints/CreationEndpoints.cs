using Application.Abstractions.Services.Saga;
using Application.Dtos.Requests;
using Application.Transactions.CreateDoctorTransaction;
using Application.Transactions.CreatePatientTransaction;
using Application.Transactions.CreateReceptionistTransaction;
using Presentation.Common;

namespace Presentation.Endpoints;

public static class CreationEndpoints
{
    public static WebApplication MapCreationEndpoints(this WebApplication app)
    {
        app.MapPost("/api/patients",
            async (CreatePatientsUnitedDto request, ITransactionsHandlerService orchestrator) => 
            { 
                var transaction = CreatePatientTransaction.MapFromRequest(request);
                
                var result = await orchestrator.StartExecuteAsync(transaction);
                
                return HttpResultBuilder.BuildResult(result); 
            } );
        
        app.MapPost("/api/doctors", 
            async (CreateDoctorsUnitedDto request, ITransactionsHandlerService orchestrator) => 
            {
                var transaction = CreateDoctorTransactions.MapFromRequest(request);
                
                var result = await orchestrator.StartExecuteAsync(transaction);
                
                return HttpResultBuilder.BuildResult(result);
            });
        
        app.MapPost("/api/receptionists",
            async (CreateReceptionistsUnitedDto request, ITransactionsHandlerService orchestrator) =>
            {
                var transaction = CreateReceptionistTransaction.MapFromRequest(request);
                
                var result = await orchestrator.StartExecuteAsync(transaction);
                
                return HttpResultBuilder.BuildResult(result);
            });
        
        return app;
    }
}