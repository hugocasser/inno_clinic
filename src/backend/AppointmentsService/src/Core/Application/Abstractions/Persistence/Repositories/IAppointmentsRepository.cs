using Domain.Models;

namespace Application.Abstractions.Persistence.Repositories;

public interface IAppointmentsRepository
{
    public void StartTransaction();
    public void Commit();
    public void Rollback();
    public Task<int> AddAsync(Appointment appointment, CancellationToken cancellationToken);
}