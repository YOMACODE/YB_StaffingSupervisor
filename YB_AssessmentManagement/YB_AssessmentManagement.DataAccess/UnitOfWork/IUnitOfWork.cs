using System;
using YB_AssessmentManagement.DataAccess.Contract;

namespace YB_AssessmentManagement.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserLogRepository UserLogRepository { get; }
        IErrorLogRepository ErrorLogRepository { get; }
        IUserRepository UserRepository { get; }
        ILeftMenuRepository LeftMenuRepository { get; }
        IUserTokensRepository UserTokensRepository { get; }
        
        void Complete();
    }
}
