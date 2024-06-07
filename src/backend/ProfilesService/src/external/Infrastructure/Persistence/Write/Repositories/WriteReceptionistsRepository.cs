using Application.Abstractions.Repositories.Write;
using Domain.Models;

namespace Infrastructure.Persistence.Write.Repositories;

public class WriteReceptionistsRepository (ProfilesWriteDbContext context)
    : WriteGenericProfilesRepository<Receptionist>(context), IWriteReceptionistsRepository;