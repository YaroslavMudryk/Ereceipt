namespace Ereceipt.Infrastructure.Data.EntityFramework.Context
{
    public class SaveChangesResult
    {
        public bool IsSuccess => Exception == null;
        public Exception Exception { get; set; }
    }
}
