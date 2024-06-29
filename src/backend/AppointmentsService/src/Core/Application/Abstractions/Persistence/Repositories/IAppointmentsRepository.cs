using Domain.Models;

namespace Application.Abstractions.Persistence.Repositories;

public interface IAppointmentsRepository
{
    public Task<int> AddAsync(Appointment appointment, CancellationToken cancellationToken);
    public Task<bool> IsExistAsync(Guid requestId, CancellationToken cancellationToken);
    public Task<int> DeleteAsync(Guid requestId, CancellationToken cancellationToken);
    public Task<int> ApproveAsync(Guid requestId, CancellationToken cancellationToken);
    public Task<bool> IsTimeFreeAsync(Guid appointmentServiceId, DateOnly appointmentDate, TimeOnly appointmentTime, int getData, CancellationToken cancellationToken);
    public Task<Appointment?> GetAsync(Guid requestAppointmentId, CancellationToken cancellationToken);
    public Task<int> UpdateDateTimeAsync(Guid requestAppointmentId, DateOnly newDate, TimeOnly newTime, CancellationToken cancellationToken);
}