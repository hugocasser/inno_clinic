using Application.ReadModels;

namespace Application.Abstractions.Repositories.Read;

public interface IReadDoctorsRepository : IReadGenericProfilesRepository<DoctorReadModel>;