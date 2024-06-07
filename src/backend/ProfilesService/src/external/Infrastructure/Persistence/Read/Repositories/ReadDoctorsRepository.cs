using Application.Abstractions.Repositories.Read;
using Application.ReadModels;

namespace Infrastructure.Persistence.Read.Repositories;

public class ReadDoctorsRepository(ProfilesReadDbContext context)
    : ReadGenericProfilesRepository<DoctorReadModel>(context), IReadDoctorsRepository;