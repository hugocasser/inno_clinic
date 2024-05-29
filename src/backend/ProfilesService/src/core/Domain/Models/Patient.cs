using Domain.Abstractions;

namespace Domain.Models;

public class Patient : Profile
{
    public bool IsLinkedToAccount { get; set; }
    public DateOnly DateOfBirth { get; set; }
}