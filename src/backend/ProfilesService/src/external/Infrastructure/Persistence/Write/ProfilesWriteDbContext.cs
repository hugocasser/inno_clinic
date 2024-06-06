using Application.Services.TransactionalOutbox;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Write;

public class ProfilesWriteDbContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; } = null!;
    public DbSet<Patient> Patients { get; set; } = null!;
    public DbSet<Receptionist> Receptionists { get; set; } = null!;
    public DbSet<OutboxMessage> OutboxMessages { get; set; } = null!;
    public DbSet<SerializedEvent> SerializedEvents { get; set; } = null!;
    public DbSet<DoctorsStatus> DoctorStatuses { get; set; } = null!;

    public ProfilesWriteDbContext(DbContextOptions<ProfilesWriteDbContext> options) : base(options){}

    public ProfilesWriteDbContext() : base(){}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProfilesWriteDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}