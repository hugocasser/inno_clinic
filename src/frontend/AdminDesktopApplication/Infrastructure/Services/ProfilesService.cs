using Application.Abstractions;
using Application.Abstractions.Services;
using Application.Dtos.Doctor;
using Application.Dtos.Patient;
using Application.Dtos.Receptionist;
using Application.Results;

namespace Infrastructure.Services;

public class ProfilesService : IProfilesService
{
    public Task<IResult> GetDoctorProfileAsync(Guid id)
    {
        var result = new Result();
        
        var doctor = new DoctorViewDto(
            id,
            null,
            "John Doe",
            DateOnly.FromDateTime(DateTime.Now),
            DateOnly.FromDateTime(DateTime.Now),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid());
        
        result.SetResultData(doctor);
        
        return Task.FromResult<IResult>(result);
    }

    public Task<IResult> GetPatientProfileAsync(Guid id)
    {
        var result = new Result();
        
        var patient = new PatientViewDto(
            id,
            null,
            "John Doe Doe",
            DateOnly.FromDateTime(DateTime.Now));
        
        result.SetResultData(patient);
        
        return Task.FromResult<IResult>(result);
    }

    public Task<IResult> GetReceptionistProfileAsync(Guid id)
    {
        var result = new Result();
        
        var receptionist = new ReceptionistViewDto(
            id,
            "John Doe Doe",
            Guid.NewGuid(),
            null);
        
        result.SetResultData(receptionist);
        
        return Task.FromResult<IResult>(result);
    }

    public Task<IResult> DeleteProfileAsync(Guid id)
    {
        return Task.FromResult<IResult>(new Result());
    }

    public Task<IResult> UpdateDoctorProfileAsync(UpdateDoctorsProfileDto doctorEditModel)
    {
        return Task.FromResult<IResult>(new Result());
    }

    public Task<IResult> CreatePatientProfileAsync(CreatePatientDto request)
    {
        return Task.FromResult<IResult>(new Result());
    }
}