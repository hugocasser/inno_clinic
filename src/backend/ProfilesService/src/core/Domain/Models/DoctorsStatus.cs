using System.Collections.ObjectModel;

namespace Domain.Models;

public class DoctorsStatus
{
    public Guid Id { get; set; }
    public string StatusName { get; set; } = string.Empty;
    public ICollection<Doctor>? Doctors { get; set; } = new Collection<Doctor>();
}