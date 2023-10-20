using System.Collections.Generic;
using System.Threading.Tasks;
using YB_AssessmentManagement.DataAccess.Entities.Model;


namespace YB_AssessmentManagement.DataAccess.Contract
{
    public interface IUserTokensRepository
    {
        Task<long> InsertOrUpdateToken(UserTokenModel userTokenModel);        
        Task<List<UserTokenModel>> GetTokensList(int UserId);
    }
}
