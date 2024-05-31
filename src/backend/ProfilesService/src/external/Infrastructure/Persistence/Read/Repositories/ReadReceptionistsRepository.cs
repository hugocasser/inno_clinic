using Application.Abstractions.Repositories.Read;
using Application.ReadModels;
using Domain.Models;

namespace Infrastructure.Persistence.Read.Repositories;

public class ReadReceptionistsRepository (ProfilesReadDbContext context) 
    : ReadGenericProfilesRepository<ReceptionistReadModel, Receptionist>(context), IReadReceptionistsRepository;