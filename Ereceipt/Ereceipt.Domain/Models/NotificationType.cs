namespace Ereceipt.Domain.Models
{
    public enum NotificationType
    {
        Welcome = 1,
        Confirm,
        Login,
        LoginCode,
        LoginAttempt,
        Logout,
        NewReceipt,
        RemoveReceipt,
        NewReceiptInGroup,
        RemoveReceiptFromGroup
    }
}