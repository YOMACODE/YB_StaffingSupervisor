namespace YB_StaffingSupervisor.DataAccess.Infrastructure
{
    public interface IConnectionFactory
    {
        DAL GetDAL { get; }
        DAL GetUserLogDAL { get; }
        DAL GetErrorLogDAL { get; }
        DAL GetSMSEmailLogDAL { get; }
        DAL GetWhatsappDAL { get; }
    }
}
