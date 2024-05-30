using Application.Abstractions.TransactionalOutbox;
using Domain.Models;

namespace Application.ReadModels;

public class ReceptionistReadModel : IReadProfileModel<Receptionist>
{
    public static ReceptionistReadModel MapToReadModel(Receptionist? entity)
    {
        return new ReceptionistReadModel();
    }
}