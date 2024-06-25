using Application.Abstractions;
using Application.Abstractions.Services;
using Application.Dtos;
using Application.Dtos.Doctor;
using Application.Dtos.Patient;
using Application.Dtos.Receptionist;
using Application.Results;
using Presentation.Abstractions.Services;
using Presentation.Abstractions.Services.PipelinedService;

namespace Presentation.Services.PipelinedServices;

public class PipelinedProfilesService(
    IProfilesService profilesService,
    ICredentialsService credentialsService) : IPipelinedProfilesService
{
    public async Task<IResult> GetDoctorProfileAsync(Guid id)
    {
        var result = new Result();
        var requestResult = await profilesService.GetDoctorProfileAsync(id);

        if (!requestResult.IsSuccess)
        {
            var response = requestResult.GetResultData<string>();

            if (response == TextResponses.Unauthorized)
            {
                var refreshResult = await credentialsService.TryRefreshTokenAsync();

                if (!refreshResult.IsSuccess)
                {
                    result.SetResultData(TextResponses.Unauthorized);

                    return result;
                }

                requestResult = await profilesService.GetDoctorProfileAsync(id);
            }

            if (!requestResult.IsSuccess)
            {
                result.SetResultData(TextResponses.SomethingWentWrong);

                return result;
            }
        }

        var dto = requestResult.GetResultData<DoctorViewDto>();

        if (dto is not null)
        {
            result.SetResultData(dto);
            return result;
        }

        result.SetResultData(TextResponses.SomethingWentWrong);

        return result;
    }

    public async Task<IResult> GetPatientProfileAsync(Guid id)
    {
        var result = new Result();
        var requestResult = await profilesService.GetPatientProfileAsync(id);

        if (!requestResult.IsSuccess)
        {
            var response = requestResult.GetResultData<string>();

            if (response == TextResponses.Unauthorized)
            {
                var refreshResult = await credentialsService.TryRefreshTokenAsync();

                if (!refreshResult.IsSuccess)
                {
                    result.SetResultData(TextResponses.Unauthorized);

                    return result;
                }

                requestResult = await profilesService.GetPatientProfileAsync(id);
            }

            if (!requestResult.IsSuccess)
            {
                result.SetResultData(TextResponses.SomethingWentWrong);

                return result;
            }
        }

        var dto = requestResult.GetResultData<PatientViewDto>();

        if (dto is not null)
        {
            result.SetResultData(dto);
            return result;
        }

        result.SetResultData(TextResponses.SomethingWentWrong);

        return result;
    }

    public async Task<IResult> GetReceptionistProfileAsync(Guid id)
    {
        var result = new Result();
        var requestResult = await profilesService.GetReceptionistProfileAsync(id);

        if (!requestResult.IsSuccess)
        {
            var response = requestResult.GetResultData<string>();

            if (response == TextResponses.Unauthorized)
            {
                var refreshResult = await credentialsService.TryRefreshTokenAsync();

                if (!refreshResult.IsSuccess)
                {
                    result.SetResultData(TextResponses.Unauthorized);

                    return result;
                }

                requestResult = await profilesService.GetReceptionistProfileAsync(id);
            }

            if (!requestResult.IsSuccess)
            {
                result.SetResultData(TextResponses.SomethingWentWrong);

                return result;
            }
        }

        var dto = requestResult.GetResultData<ReceptionistViewDto>();

        if (dto is not null)
        {
            result.SetResultData(dto);
            return result;
        }

        result.SetResultData(TextResponses.SomethingWentWrong);

        return result;
    }

    public async Task<IResult> DeleteProfileAsync(Guid id)
    {
        var result = new Result();
        var requestResult = await profilesService.DeleteProfileAsync(id);

        if (requestResult.IsSuccess)
        {
            return result;
        }

        var response = requestResult.GetResultData<string>();

        if (response == TextResponses.Unauthorized)
        {
            var refreshResult = await credentialsService.TryRefreshTokenAsync();

            if (!refreshResult.IsSuccess)
            {
                result.SetResultData(TextResponses.Unauthorized);

                return result;
            }

            requestResult = await profilesService.DeleteProfileAsync(id);
        }

        if (requestResult.IsSuccess)
        {
            return result;
        }

        result.SetResultData(TextResponses.SomethingWentWrong);

        return result;
    }

    public async Task<IResult> UpdateDoctorProfileAsync(UpdateDoctorsProfileDto request)
    {
        var result = new Result();
        var requestResult = await profilesService.UpdateDoctorProfileAsync(request);

        if (!requestResult.IsSuccess)
        {
            var response = requestResult.GetResultData<string>();

            if (response == TextResponses.Unauthorized)
            {
                var refreshResult = await credentialsService.TryRefreshTokenAsync();

                if (!refreshResult.IsSuccess)
                {
                    result.SetResultData(TextResponses.Unauthorized);

                    return result;
                }

                requestResult = await profilesService.UpdateDoctorProfileAsync(request);
            }

            if (!requestResult.IsSuccess)
            {
                result.SetResultData(TextResponses.SomethingWentWrong);

                return result;
            }
        }

        var dto = requestResult.GetResultData<DoctorViewDto>();

        if (dto is not null)
        {
            result.SetResultData(dto);
            return result;
        }

        result.SetResultData(TextResponses.SomethingWentWrong);

        return result;
    }

    public async Task<IResult> CreatePatientProfileAsync(CreatePatientDto request)
    {
        var result = new Result();
        var requestResult = await profilesService.CreatePatientProfileAsync(request);

        if (requestResult.IsSuccess)
        {
            return result;
        }

        var response = requestResult.GetResultData<string>();

        switch (response)
        {
            case TextResponses.Unauthorized:
            {
                var refreshResult = await credentialsService.TryRefreshTokenAsync();

                if (!refreshResult.IsSuccess)
                {
                    result.SetResultData(TextResponses.Unauthorized);

                    return result;
                }

                requestResult = await profilesService.CreatePatientProfileAsync(request);
                break;
            }
            case TextResponses.EmailAlreadyExists:
            {
                result.SetResultData(TextResponses.EmailAlreadyExists);

                return result;
            }
        }

        if (requestResult.IsSuccess)
        {
            return result;
        }

        result.SetResultData(TextResponses.SomethingWentWrong);

        return result;
    }

    public async Task<IResult> CreateReceptionistAsync(CreateReceptionistDto request)
    {
        var result = new Result();
        var requestResult = await profilesService.CreateReceptionistAsync(request);

        if (requestResult.IsSuccess)
        {
            return result;
        }

        var response = requestResult.GetResultData<string>();

        switch (response)
        {
            case TextResponses.Unauthorized:
            {
                var refreshResult = await credentialsService.TryRefreshTokenAsync();

                if (!refreshResult.IsSuccess)
                {
                    result.SetResultData(TextResponses.Unauthorized);

                    return result;
                }

                requestResult = await profilesService.CreateReceptionistAsync(request);
                break;
            }
            case TextResponses.EmailAlreadyExists:
            {
                result.SetResultData(TextResponses.EmailAlreadyExists);

                return result;
            }
        }

        if (requestResult.IsSuccess)
        {
            return result;
        }

        result.SetResultData(TextResponses.SomethingWentWrong);

        return result;
    }

    public async Task<IResult> CreateDoctorProfileAsync(CreateDoctorsProfileDto request)
    {
        var result = new Result();
        var requestResult = await profilesService.CreateDoctorProfileAsync(request);

        if (requestResult.IsSuccess)
        {
            return result;
        }

        var response = requestResult.GetResultData<string>();

        switch (response)
        {
            case TextResponses.Unauthorized:
            {
                var refreshResult = await credentialsService.TryRefreshTokenAsync();

                if (!refreshResult.IsSuccess)
                {
                    result.SetResultData(TextResponses.Unauthorized);

                    return result;
                }

                requestResult = await profilesService.CreateDoctorProfileAsync(request);
                break;
            }
            case TextResponses.EmailAlreadyExists:
            {
                result.SetResultData(TextResponses.EmailAlreadyExists);

                return result;
            }
        }

        if (requestResult.IsSuccess)
        {
            return result;
        }

        result.SetResultData(TextResponses.SomethingWentWrong);

        return result;
    }
}