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
		IMyTeamRepository MyTeamRepository { get; }
		ILeaveApprovalsRepository LeaveApprovalsRepository { get; }
		IClaimRequestsRepository ClaimRequestsRepository { get; }
        IOnDutyRequesteRepository OnDutyRequesteRepository { get; }
        IUserClaimRequestsRepository UserClaimRequestsRepository { get; }
        void Complete();
    }
}
