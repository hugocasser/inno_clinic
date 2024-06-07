using Application.Abstractions.TransactionalOutbox;
using Domain.Models;

namespace Application.ReadModels;

public class ReceptionistReadModel : IReadProfileModel
{
    public static ReceptionistReadModel MapToReadModel(Receptionist? entity)
    {
        return new ReceptionistReadModel();
    }

    public Guid Id { get; set; }
    public string FullName { get; set; } = null!;
    public bool IsDeleted { get; set; } = false;
}