namespace Application.Dtos.Requests;

public class FullResultDto
{
    public const string Request = """
                                  SELECT
                                      r.id AS result_id,
                                      r.complaints,
                                      r.conclusion,
                                      r.recommendation,
                                      r.appointment_id,
                                      a.id AS appointment_id,
                                      a.doctor_id,
                                      a.patient_id,
                                      a.service_id,
                                      a.appointment_date,
                                      a.appointment_time,
                                      a.is_approved
                                  FROM
                                      results r
                                  JOIN
                                      appointments a ON r.appointment_id = a.id;
                                  """;

    public Guid Id { get; set; }
    public string Complaints { get; set; } = string.Empty;
    public string Conclusion { get; set; } = string.Empty;
    public string Recommendation { get; set; } = string.Empty;
    public Guid AppointmentId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid PatientId { get; set; }
    public Guid ServiceId { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public bool IsApproved { get; set; }
}