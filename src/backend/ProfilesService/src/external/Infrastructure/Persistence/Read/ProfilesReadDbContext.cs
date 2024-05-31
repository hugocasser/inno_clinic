using Application.Abstractions.TransactionalOutbox;
using Application.ReadModels;
using Domain.Abstractions;
using Infrastructure.Options;
using Infrastructure.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Read;

public class ProfilesReadDbContext
{
    private readonly IMongoCollection<DoctorReadModel> _doctors;
    private readonly IMongoCollection<PatientReadModel> _patients;
    private readonly IMongoCollection<ReceptionistReadModel> _receptionists;


    public ProfilesReadDbContext(IOptions<MongoOptions> options)
    {
        var client = new CustomMongoClient(options);
        var database = client.GetDatabase(options.Value.DatabaseName);
        
        _doctors = database.GetCollection<DoctorReadModel>(options.Value.Collections[0]);
        _patients = database.GetCollection<PatientReadModel>(options.Value.Collections[1]);
        _receptionists = database.GetCollection<ReceptionistReadModel>(options.Value.Collections[2]);
    }


    public IMongoCollection<TReadModel> Collection<TReadModel, TModel>()
        where TReadModel : class, IReadProfileModel<TModel>
        where TModel : Profile
    {
        if (typeof(TReadModel) == typeof(DoctorReadModel))
        {
            return (_doctors as IMongoCollection<TReadModel>)!;
        }

        if (typeof(TReadModel) == typeof(PatientReadModel))
        {
            return (_patients as IMongoCollection<TReadModel>)!;
        }

        if (typeof(TReadModel) == typeof(ReceptionistReadModel))
        {
            return (_receptionists as IMongoCollection<TReadModel>)!;
        }
        
        throw new ArgumentOutOfRangeException();
    }
}