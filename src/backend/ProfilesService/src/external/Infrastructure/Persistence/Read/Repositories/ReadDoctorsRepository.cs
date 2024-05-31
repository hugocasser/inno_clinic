using Application.Abstractions.Repositories.Read;
using Application.ReadModels;
using Domain.Models;

namespace Infrastructure.Persistence.Read.Repositories;

public class ReadDoctorsRepository(ProfilesReadDbContext context)
    : ReadGenericProfilesRepository<DoctorReadModel, Doctor>(context), IReadDoctorsRepository;