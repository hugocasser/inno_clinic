using Application.Abstractions.Repositories.Write;
using Domain.Models;

namespace Infrastructure.Persistence.Write.Repositories;

public class WritePatientsRepository(ProfilesWriteDbContext context)
    : WriteGenericProfilesRepository<Patient>(context), IWritePatientsRepository;