using YB_AssessmentManagement.DataAccess.Infrastructure;


namespace YB_AssessmentManagement.DataAccess.Repository
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
