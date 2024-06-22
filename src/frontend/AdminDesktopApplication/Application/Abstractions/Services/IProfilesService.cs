using Application.Dtos;

namespace Application.Abstractions.Services;

public interface IProfilesService
{
    public Task<IResult> GetDoctorProfileAsync(Guid id);
    public Task<IResult> GetPatientProfileAsync(Guid id);
    public Task<IResult> GetReceptionistProfileAsync(Guid id);
    
    public Task<IResult> DeleteProfileAsync(Guid id);
    Task<IResult> UpdateDoctorProfileAsync(UpdateDoctorsProfileDto doctorEditModel);
}