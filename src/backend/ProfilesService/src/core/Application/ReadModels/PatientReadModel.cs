using Application.Abstractions.TransactionalOutbox;
using Domain.Models;

namespace Application.ReadModels;

public class PatientReadModel : IReadProfileModel<Patient>
{
    public static PatientReadModel MapToReadModel(Patient entity)
    {
        return new PatientReadModel();
    }
}