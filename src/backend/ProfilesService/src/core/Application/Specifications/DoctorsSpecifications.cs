using Application.ReadModels;
using MongoDB.Driver;

namespace Application.Specifications;

public static class DoctorsSpecifications
{
    private static readonly FilterDefinitionBuilder<DoctorReadModel> _builder = new ();
    private static readonly FilterDefinition<DoctorReadModel> _notDeleted = _builder.Eq(doctor => doctor.IsDeleted, false);
    
    public static FilterDefinition<DoctorReadModel> ByOfficeNotDeleted(Guid officeId)
    {
        return _builder.And(Office(officeId) , _notDeleted);
    }

    public static FilterDefinition<DoctorReadModel> ByIdNotDeleted(Guid id)
    {
        return _builder.And(Id(id), _notDeleted);
    }

    public static FilterDefinition<DoctorReadModel> SpecializationNotDeleted(string specialization)
    {
        return _builder.And(Specialization(specialization), _notDeleted);
    }
    
    public static FilterDefinition<DoctorReadModel> ByPatientNotDeleted(int minExp, int maxExp)
    {
        return _builder.And(ByPatient(minExp, maxExp), _notDeleted);
    }
    
    private static FilterDefinition<DoctorReadModel> Office(Guid officeId)
    {
        return _builder.Eq(x => x.OfficeId, officeId);
    }
    
    private static FilterDefinition<DoctorReadModel> Id(Guid id)
    {
        return _builder.Eq(x => x.Id, id);
    }
    
    private static FilterDefinition<DoctorReadModel> Specialization(string specialization)
    {
        return _builder.Eq(x => x.Specialization, specialization);
    }

    private static FilterDefinition<DoctorReadModel> ByPatient(int minExp, int maxExp)
    {
        if (maxExp == 0)
        {
            return _builder.Empty;
        }
        
        var currentYear = DateTime.Now.Year;
        var min = currentYear - minExp;
        var max = currentYear + maxExp;
        
        return _builder.Gte(x => x.CareerStarted.Year, min) 
            & _builder.Lte(x => x.CareerStarted.Year, max);
    }
}