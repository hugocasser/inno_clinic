using System.Collections.Frozen;
using Application.Abstractions.Repositories.OutBox;
using Application.Abstractions.TransactionalOutbox;
using Application.Services.TransactionalOutbox;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Application.BackgroundJobs;

public class OutboxMessageProcessingJob
    (ILogger<OutboxMessageProcessingJob> logger,
        IOutboxMessageProcessor messageProcessor,
        IOutboxMessagesRepository<OutboxMessage> outboxMessagesRepository): IJob
{
    private const string SuccessfullyProcessed = "Successfully processed message ";
    private const string FailedToProcess = "Failed to process message, error was: ";
    
    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Starting processing of outbox messages at {time}", DateTimeOffset.Now);
        
        var token = context.CancellationToken;
        var messages = await outboxMessagesRepository.GetNotProcessedAsync(50, token);
        
        if (messages is null)
        {
            logger.LogInformation("No messages to process");
            
            return;
        }

        var successfullyCount = 0;
        var failedCount = 0;
        
        await foreach (var result in messageProcessor.ProcessAsync(messages.ToFrozenSet(), token))
        {
            if (result.IsSuccess)
            {
                successfullyCount++;
                logger.LogInformation(SuccessfullyProcessed + "{messageId}", result.GetTypedContent().Id);
            }
            else
            {
                failedCount++;
                logger.LogError(FailedToProcess + "{error}", result.GetErrors().First().Message);
            }
        }
        
        logger.LogInformation("Finished processing of outbox messages at {time}", DateTimeOffset.Now);
        logger.LogInformation("Successfully processed {count} messages", successfullyCount);
        logger.LogInformation("Failed to process {count} messages", failedCount);
    }
    
    
}