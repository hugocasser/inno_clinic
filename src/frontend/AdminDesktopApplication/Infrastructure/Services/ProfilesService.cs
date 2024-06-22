using Application.Abstractions;
using Application.Abstractions.Services;
using Application.Dtos;
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
            "John",
            "Doe",
            null,
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
        return Task.FromResult<IResult>(new Result());
    }

    public Task<IResult> GetReceptionistProfileAsync(Guid id)
    {
        return Task.FromResult<IResult>(new Result());
    }

    public Task<IResult> DeleteProfileAsync(Guid id)
    {
        return Task.FromResult<IResult>(new Result());
    }

    public Task<IResult> UpdateDoctorProfileAsync(UpdateDoctorsProfileDto doctorEditModel)
    {
        return Task.FromResult<IResult>(new Result());
    }
}