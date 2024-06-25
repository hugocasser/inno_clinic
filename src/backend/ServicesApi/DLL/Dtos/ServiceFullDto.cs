namespace DLL.Dtos;

public class ServiceFullDto
{
    public Guid ServiceId { get; set; }
    public string ServiceName { get; set; }
    public int Price { get; set; }
    public bool ServiceIsActive { get; set; }
    public int TimeSlotSize { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }
    public Guid SpecializationId { get; set; }
    public string SpecializationName { get; set; }
    public bool SpecializationIsActive { get; set; }
}