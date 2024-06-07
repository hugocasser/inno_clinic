using Application.Abstractions.Persistence.Repositories;
using Application.Abstractions.Services.TransactionalOutboxServices;
using Domain.Abstractions.Events;
using Domain.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.IO;
using Quartz;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace Application.BackgroundJobs;

public class ProcessOutboxMessagesJob
    (IWriteOfficesRepository writeOfficesRepository,
        IDomainEventProcessorService domainEventProcessorService,
        ILogger<ProcessOutboxMessagesJob> logger) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Event processing started");

        var messages = await writeOfficesRepository.GetNotProcessedMessagesAsync(50);

        foreach (var message in messages)
        {
            await domainEventProcessorService
                .ProcessDomainEventMessagesAsync
                    (JsonConvert.DeserializeObject<IDomainEvent<Office>>(message.SerializedEvent!)
                        !, context.CancellationToken);
            
            message.ProcessedAt = DateTime.Now;
        }
        
        await writeOfficesRepository
            .SetProcessedAtRangeAsync(messages);
        
        await writeOfficesRepository
            .SaveChangesAsync(context.CancellationToken);
        
        logger.LogInformation("Event processing finished");
    }
}