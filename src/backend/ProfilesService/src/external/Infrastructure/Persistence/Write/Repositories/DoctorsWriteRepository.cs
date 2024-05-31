using Application.Abstractions.Repositories.Write;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Write.Repositories;

public class DoctorsWriteRepository(DbContext context)
    : GenericProfilesWriteRepository<Doctor>(context), IDoctorsWriteRepository;