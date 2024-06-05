using System.Collections.Frozen;
using Application.Abstractions.Repositories.Outbox;
using Application.Abstractions.TransactionalOutbox;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Application.BackgroundJobs;

public class OutboxMessageProcessingJob
    (ILogger<OutboxMessageProcessingJob> logger,
        IOutboxMessageProcessor messageProcessor,
        IOutboxMessagesRepository outboxMessagesRepository): IJob
{
    private const string SuccessfullyProcessed = "Successfully processed message ";
    private const string FailedToProcess = "Failed to process message, error was: ";
    
    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Starting processing of outbox messages at {time}", DateTimeOffset.Now);
        
        var token = context.CancellationToken;
        var messages = await outboxMessagesRepository.GetNotProcessedAsync(50, token);
        
        if (messages.Count != 0)
        {
            logger.LogInformation("No messages to process");
            
            return;
        }

        var successfullyProcessedCount = 0;
        var unsuccessfullyProcessedCount = 0;

        await foreach (var result in messageProcessor.ProcessAsync(messages.ToFrozenSet(), token))
        {
            if (result.IsSuccess)
            {
                successfullyProcessedCount++;
                logger.LogInformation(SuccessfullyProcessed + "{messageId}", result.GetTypedContent().Id);
            }
            else
            {
                unsuccessfullyProcessedCount++;
                logger.LogError(FailedToProcess + "{error}", result.ErrorsToString());
            }
        }
        
        logger.LogInformation("Finished processing of outbox messages at {time}", DateTimeOffset.Now);
        logger.LogInformation("Successfully processed {count} messages", successfullyProcessedCount);
        logger.LogInformation("Failed to process {count} messages", unsuccessfullyProcessedCount);
    }
    
    
}