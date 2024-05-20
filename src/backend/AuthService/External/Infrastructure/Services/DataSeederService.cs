using Application.Abstractions.Services;
using Domain.Models;
using Infrastructure.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

public class DataSeederService(UserManager<User> userManager, RoleManager<UserRole> roleManager,
    IOptions<ReceptionistSeedOptions> receptionistSeedOptions) : IDataSeederService
{
    public async Task SeedRecordsAsync(CancellationToken cancellationToken = default)
    {
        var receptionist = new User
        {
            Id = Guid.NewGuid(),
            Email = receptionistSeedOptions.Value.Email,
            UserName = receptionistSeedOptions.Value.Name,
            EmailConfirmed = true
        };

        var patientRole = new UserRole()
        {
            Id = Guid.NewGuid(),
            Name = nameof(Roles.Patient)
        };

        var doctorRole = new UserRole()
        {
            Id = Guid.NewGuid(),
            Name = nameof(Roles.Doctor)
        };

        var receptionistRole = new UserRole()
        {
            Id = Guid.NewGuid(),
            Name = nameof(Roles.Receptionist)
        };

        await userManager.CreateAsync(receptionist, receptionistSeedOptions.Value.Password);
        await roleManager.CreateAsync(patientRole);
        await roleManager.CreateAsync(doctorRole);
        await roleManager.CreateAsync(receptionistRole);
        
        await userManager.AddToRoleAsync(receptionist, nameof(Roles.Receptionist));
    }
}