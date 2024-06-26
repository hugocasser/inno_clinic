using Domain.Models;

namespace Application.Abstractions.Persistence.Repositories;

public interface IAppointmentsRepository
{
    public void StartTransaction();
    public void Commit();
    public void Rollback();
    public Task<int> AddAsync(Appointment appointment, CancellationToken cancellationToken);
    public Task<bool> IsTimeFreeAsync(DateOnly date, TimeOnly time, CancellationToken cancellationToken);
    Task<bool> IsExistAsync(Guid requestId, CancellationToken cancellationToken);
    Task<int> DeleteAsync(Guid requestId, CancellationToken cancellationToken);
    Task<int> ApproveAsync(Guid requestId, CancellationToken cancellationToken);
}