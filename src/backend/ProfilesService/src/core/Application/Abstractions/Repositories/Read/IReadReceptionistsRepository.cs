using Application.ReadModels;
using Domain.Models;

namespace Application.Abstractions.Repositories.Read;

public interface IReadReceptionistsRepository : IReadGenericProfilesRepository<ReceptionistReadModel, Receptionist>;