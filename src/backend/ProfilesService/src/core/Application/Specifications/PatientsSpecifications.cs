using Application.ReadModels;
using MongoDB.Driver;

namespace Application.Specifications;

public static class PatientsSpecifications
{
    private static readonly FilterDefinitionBuilder<PatientReadModel> _builder = new ();
    private static readonly FilterDefinition<PatientReadModel> _notDeleted = _builder.Eq(patient => patient.IsDeleted, false);
    
    public static FilterDefinition<PatientReadModel> ByIdNotDeleted(Guid id)
    {
        return _builder.And(Id(id), _notDeleted);
    }
    private static FilterDefinition<PatientReadModel> Id(Guid id)
    {
        return _builder.Eq(x => x.Id, id);
    }
}