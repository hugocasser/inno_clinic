using Application.ReadModels;
using Domain.Models;

namespace Application.Abstractions.Repositories.Read;

public interface IReceptionistReadRepository : IReadGenericRepository<ReceptionistReadModel, Receptionist>
{
    
}