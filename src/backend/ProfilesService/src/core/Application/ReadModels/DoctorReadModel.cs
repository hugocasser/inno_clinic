using Application.Abstractions.TransactionalOutbox;
using Domain.Models;

namespace Application.ReadModels;

public class DoctorReadModel : IReadProfileModel<Doctor>
{
    public static DoctorReadModel MapToReadModel(Doctor? entity)
    {
        return new DoctorReadModel();
    }
}