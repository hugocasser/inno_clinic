using Application.Abstractions.TransactionalOutbox;
using Application.ReadModels;
using Domain.Abstractions;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Read;

public class ProfilesReadDbContext
{
    private readonly IMongoCollection<DoctorReadModel> _doctors;
    private readonly IMongoCollection<PatientReadModel> _patients;
    private readonly IMongoCollection<ReceptionistReadModel> _receptionists;


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