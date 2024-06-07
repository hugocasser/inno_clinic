using Domain.Models;

namespace Application.Abstractions.Repositories.Write;

public interface IWritePatientsRepository : IWriteGenericProfilesRepository<Patient>;