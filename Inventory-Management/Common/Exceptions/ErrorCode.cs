namespace Inventory_Management.Common.Exceptions
{
    public enum ErrorCode
    {
        None,
        UnKnown,
        // USER
        EmailIsNotFound,
        WrongPassword,
        PasswordsDontMatch,
        EmailAlreadyExist,
        UserNameAlreadyExist,
        WrongPasswordOrEmail,
        UnableTogenerateToken,
        RoleNotFound,
        OtpIsNotValid,
        OtpIsNotFound
    }
}
