using Application.Abstractions.Repositories.Read;
using Application.ReadModels;

namespace Infrastructure.Persistence.Read.Repositories;

public class ReadPatientsRepository(ProfilesReadDbContext context)
    : ReadGenericProfilesRepository<PatientReadModel>(context), IReadPatientsRepository;