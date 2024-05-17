using Application.Abstractions.Results;
using Domain.Models;

namespace Application.Abstractions.Auth;

public interface IUserService
{
    public Task<IResult> RegisterUser(string email, string password, Roles role);
}