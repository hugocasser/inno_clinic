using Application.Abstractions.TransactionalOutbox;
using Domain.Models;

namespace Application.ReadModels;

public class PatientReadModel : IReadProfileModel
{
    public static PatientReadModel MapToReadModel(Patient entity)
    {
        return new PatientReadModel();
    }

    public Guid Id { get; set; }
    public string FullName { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateOnly Birthday { get; set; }
}