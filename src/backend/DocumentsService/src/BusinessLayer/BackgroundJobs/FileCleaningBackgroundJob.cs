using BusinessLayer.Abstractions.Services;
using Quartz;

namespace BusinessLayer.BackgroundJobs;

public class FileCleaningBackgroundJob
    (IFilesCleanerService cleanerService): IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await cleanerService.CleanAsync(context.CancellationToken);
    }
}