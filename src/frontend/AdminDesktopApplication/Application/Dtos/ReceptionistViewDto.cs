namespace Application.Dtos;

public class ReceptionistViewDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = null!;
    public Guid OfficeId { get; set; }
}