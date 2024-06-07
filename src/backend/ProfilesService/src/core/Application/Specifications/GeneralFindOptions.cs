using Application.Dtos.Requests;
using MongoDB.Driver;

namespace Application.Specifications;

public static class GeneralFindOptions
{
    public static FindOptions<T> FromPageSettings<T>(PageSettings pageSettings)
    {
        return new FindOptions<T>
        {
            Skip = (pageSettings.PageNumber -1) * pageSettings.PageSize,
            Limit = pageSettings.PageSize
        };
    }
}