using Application.ReadModels;
using Domain.Models;

namespace Application.Abstractions.Repositories.Read;

public interface IReadPatientRepository : IReadGenericRepository<PatientReadModel, Patient>
{
    
}