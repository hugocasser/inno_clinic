using Application.Abstractions.Repositories.Read;
using Application.ReadModels;

namespace Infrastructure.Persistence.Read.Repositories;

public class ReadReceptionistsRepository (ProfilesReadDbContext context) 
    : ReadGenericProfilesRepository<ReceptionistReadModel>(context), IReadReceptionistsRepository;