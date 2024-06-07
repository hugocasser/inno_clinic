using Domain.Models;
using Infrastructure.Persistence.Write;

namespace Presentation.Common;

public static class DataSeeder
{
    public static async Task SeedStatuses(ProfilesWriteDbContext context)
    {
        var statuses = new List<DoctorsStatus>
        {
            new ()
            {
                Id = Guid.NewGuid(),
                StatusName = nameof(StatusesEnum.AtWork)
            },
            new ()
            {
                Id = Guid.NewGuid(),
                StatusName = nameof(StatusesEnum.OnVacation)
            },
            new ()
            {
                Id = Guid.NewGuid(),
                StatusName = nameof(StatusesEnum.SickDay)
            },
            new ()
            {
                Id = Guid.NewGuid(),
                StatusName = nameof(StatusesEnum.SickLeave)
            },
            new ()
            {
                Id = Guid.NewGuid(),
                StatusName = nameof(StatusesEnum.LiveWithoutPay)
            },
            new ()
            {
                Id = Guid.NewGuid(),
                StatusName = nameof(StatusesEnum.Inactive)
            },
        };
        
        
        await context.DoctorStatuses.AddRangeAsync(statuses);
        await context.SaveChangesAsync();
    }
}