namespace Application.OperationResult.Errors;

public static class ErrorMessages
{
    public const string InvalidEmailOrPassword = "Invalid email or password";
    public const string UserAlreadyExists = "User already exists";
    public const string UserNotHaveSuitableRole = "User not have suitable role";
    public const string InvalidRefreshToken = "Invalid refresh token, please login again";
    public const string AccessDenied = "You can't access other user data";
    public const string UserNotFound = "User not found";
    public const string InvalidConfirmToken = "Invalid confirm token";
    public const string EmailAlreadyConfirmed = "Email already confirmed";
}