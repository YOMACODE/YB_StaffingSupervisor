using System;
using YB_StaffingSupervisor.DataAccess.Contract;

namespace YB_StaffingSupervisor.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserLogRepository UserLogRepository { get; }
        IErrorLogRepository ErrorLogRepository { get; }
        IUserRepository UserRepository { get; }
        ILeftMenuRepository LeftMenuRepository { get; }
        IUserTokensRepository UserTokensRepository { get; }
        IDesignationRepository DesignationRepository { get; }
        IMyTeamRepository MyTeamRepository { get; }
        void Complete();
    }
}
