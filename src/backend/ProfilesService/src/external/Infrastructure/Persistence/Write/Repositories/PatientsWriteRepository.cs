using Application.Abstractions.Repositories.Write;
using Domain.Models;

namespace Infrastructure.Persistence.Write.Repositories;

public class PatientsWriteRepository(ProfilesWriteDbContext context)
    : GenericProfilesWriteRepository<Patient>(context), IPatientWriteRepository;