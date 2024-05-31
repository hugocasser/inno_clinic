using Application.Abstractions.Repositories.Write;
using Domain.Models;

namespace Infrastructure.Persistence.Write.Repositories;

public class ReceptionistsWriteRepository (ProfilesWriteDbContext context)
    : GenericProfilesWriteRepository<Receptionist>(context), IReceptionistsWriteRepository;