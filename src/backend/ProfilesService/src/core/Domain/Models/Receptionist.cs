using Domain.Abstractions;

namespace Domain.Models;

public class Receptionist : Profile
{
    public Guid OfficeId { get; set; }
}