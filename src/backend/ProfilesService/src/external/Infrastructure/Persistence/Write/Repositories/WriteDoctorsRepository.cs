using Application.Abstractions.Repositories.Write;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Write.Repositories;

public class WriteDoctorsRepository(DbContext context)
    : WriteGenericProfilesRepository<Doctor>(context), IWriteDoctorsRepository;