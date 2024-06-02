using Application.Abstractions.TransactionalOutbox;
using Domain.Models;

namespace Application.ReadModels;

public class DoctorReadModel : IReadProfileModel<Doctor>
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public DateOnly CareerStarted { get; set; }
    public Guid OfficeId { get; set; }
    public string Status { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
    public static DoctorReadModel MapToReadModel(Doctor entity)
    {
        var readModel = new DoctorReadModel()
        {
            Id = entity.Id,
            FullName = entity.LastName + " " + entity.FirstName + " " + entity.MiddleName,
            BirthDate = entity.DateOfBirth,
            CareerStarted = entity.StartedCareer,
            OfficeId = entity.OfficeId,
            Status = entity.Status.StatusName,
            IsDeleted = entity.IsDeleted
        };
        
        return readModel;
    }
}