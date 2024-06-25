using System.Data;
using DLL.Abstractions.Persistence.Repositories;
using DLL.Options;
using Microsoft.Extensions.Options;

namespace DLL.Persistence.Repositories;

public class ServicesRepository(IOptions<PostgresOptions> options) :
    BaseRepository(options), IServicesRepository
{
}