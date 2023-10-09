using System.Collections.Generic;
using System.Threading.Tasks;
using YB_StaffingSupervisor.DataAccess.Entities.Model;


namespace YB_StaffingSupervisor.DataAccess.Contract
{
    public interface IUserTokensRepository
    {
        Task<long> InsertOrUpdateToken(UserTokenModel userTokenModel);        
        Task<List<UserTokenModel>> GetTokensList(int UserId);
    }
}
