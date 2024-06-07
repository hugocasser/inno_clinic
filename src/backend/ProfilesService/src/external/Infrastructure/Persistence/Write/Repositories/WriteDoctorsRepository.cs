using Application.Abstractions.Repositories.Write;
using Domain.Models;

namespace Infrastructure.Persistence.Write.Repositories;

public class WriteDoctorsRepository(ProfilesWriteDbContext context)
    : WriteGenericProfilesRepository<Doctor>(context), IWriteDoctorsRepository;