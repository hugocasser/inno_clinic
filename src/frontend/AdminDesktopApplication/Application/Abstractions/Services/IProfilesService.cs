using Application.Dtos.Doctor;
using Application.Dtos.Patient;
using Application.Dtos.Receptionist;

namespace Application.Abstractions.Services;

public interface IProfilesService
{
    public Task<IResult> GetDoctorProfileAsync(Guid id);
    public Task<IResult> GetPatientProfileAsync(Guid id);
    public Task<IResult> GetReceptionistProfileAsync(Guid id);
    public Task<IResult> DeleteProfileAsync(Guid id);
    public Task<IResult> UpdateDoctorProfileAsync(UpdateDoctorsProfileDto request);
    public Task<IResult> CreatePatientProfileAsync(CreatePatientDto request);
    public Task<IResult> CreateReceptionistAsync(CreateReceptionistDto request);
}