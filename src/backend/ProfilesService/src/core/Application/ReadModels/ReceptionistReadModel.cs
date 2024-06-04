using Application.Abstractions.TransactionalOutbox;
using Domain.Models;

namespace Application.ReadModels;

public class ReceptionistReadModel : IReadProfileModel<Receptionist>
{
    public static ReceptionistReadModel MapToReadModel(Receptionist? entity)
    {
        return new ReceptionistReadModel();
    }

    public Guid Id { get; set; }
    public string FullName { get; set; }
    public bool IsDeleted { get; set; } = false;
}