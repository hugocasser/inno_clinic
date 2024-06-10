using Application.Dtos.Common;

namespace Application.TransactionComponents.RegisterUserComponent;

public interface ITransactionWithUserRegistration
{
    public string Email { get; set; }
    public string Password { get; set; }
    public EnumRoles Role { get; set; }
    public Guid AccountId { get; set; }
    public void SetAccountId(Guid accountId);
}