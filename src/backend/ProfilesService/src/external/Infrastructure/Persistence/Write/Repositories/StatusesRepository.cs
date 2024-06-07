using Application.Abstractions.Repositories.Write;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Write.Repositories;

public class StatusesRepository(ProfilesWriteDbContext context) : IStatusesRepository
{
    public async Task CreateAsync(DoctorsStatus status, CancellationToken cancellationToken = default)
    {
        await context.DoctorStatuses.AddAsync(status, cancellationToken);
    }

    public async Task<DoctorsStatus?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context
            .DoctorStatuses
            .FirstOrDefaultAsync(status => status.Id == id, cancellationToken);
    }
}