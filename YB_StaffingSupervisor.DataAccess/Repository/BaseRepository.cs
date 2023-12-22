using YB_StaffingSupervisor.DataAccess.Infrastructure;


namespace YB_StaffingSupervisor.DataAccess.Repository
{
    public abstract class BaseRepository
    {
        protected readonly IConnectionFactory connectionFactory;        
        public BaseRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }       
    }
}
