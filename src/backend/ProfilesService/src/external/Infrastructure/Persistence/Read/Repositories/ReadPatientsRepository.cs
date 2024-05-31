using Application.Abstractions.Repositories.Read;
using Application.ReadModels;
using Domain.Models;

namespace Infrastructure.Persistence.Read.Repositories;

public class ReadPatientsRepository(ProfilesReadDbContext context)
    : ReadGenericProfilesRepository<PatientReadModel, Patient>(context), IReadPatientsRepository;