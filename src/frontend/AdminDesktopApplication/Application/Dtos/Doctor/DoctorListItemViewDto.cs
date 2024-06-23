namespace Application.Dtos.Doctor;

public class DoctorListItemViewDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public string Photo { get; set; } = null!;
    public string Specialization { get; set; } = null!;
    public string Status { get; set; } = null!;
}