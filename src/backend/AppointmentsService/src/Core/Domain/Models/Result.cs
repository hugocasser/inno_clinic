namespace Domain.Models;

public class Result
{
    public Guid Id { get; set; }
    public string Complaints { get; set; } = string.Empty;
    public string Conclusion { get; set; } = string.Empty;
    public string Recommendation { get; set; } = string.Empty;
    public Guid AppointmentId { get; set; }
    public bool IsDeleted { get; set; }
}