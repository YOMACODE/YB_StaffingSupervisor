
using System;
using YB_StaffingSupervisor.DataAccess.Contract;
using YB_StaffingSupervisor.DataAccess.Infrastructure;
using YB_StaffingSupervisor.DataAccess.Repository;

namespace YB_StaffingSupervisor.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IConnectionFactory _connectionFactory;
        public UnitOfWork(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        #region  IErrorLogRepository 
        public IErrorLogRepository _errorLogRepository;
        public IErrorLogRepository ErrorLogRepository
        {
            get
            {
                if (_errorLogRepository == null)
                {
                    _errorLogRepository = new ErrorLogRepository(_connectionFactory);
                }
                return _errorLogRepository;
            }
        }
        #endregion

        #region  IUserLogRepository 
        public IUserLogRepository _userLogRepository;
        public IUserLogRepository UserLogRepository
        {
            get
            {
                if (_userLogRepository == null)
                {
                    _userLogRepository = new UserLogRepository(_connectionFactory);
                }
                return _userLogRepository;
            }
        }
        #endregion

        #region  IUserRepository 
        public IUserRepository _userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_connectionFactory);
                }
                return _userRepository;
            }
        }
        #endregion

        #region ILeftMenuRepository 
        public ILeftMenuRepository _leftMenuRepository;
        public ILeftMenuRepository LeftMenuRepository
        {
            get
            {
                if (_leftMenuRepository == null)
                {
                    _leftMenuRepository = new LeftMenuRepository(_connectionFactory);
                }
                return _leftMenuRepository;
            }
        }
        #endregion
        #region IUserTokensRepository
        IUserTokensRepository _userTokensRepository;
        public IUserTokensRepository UserTokensRepository
        {
            get
            {
                if (_userTokensRepository == null)
                {
                    _userTokensRepository = new UserTokensRepository(_connectionFactory);
                }
                return _userTokensRepository;
            }
        }
		#endregion
		#region IMyTeamRepository
		IMyTeamRepository _myTeamRepository;
		public IMyTeamRepository MyTeamRepository
		{
			get
			{
				if (_myTeamRepository == null)
				{
					_myTeamRepository = new MyTeamRepository(_connectionFactory);
				}
				return _myTeamRepository;
			}
		}
        #endregion


        #region ILeaveApprovalsRepository
        ILeaveApprovalsRepository _leaveApprovalsRepository;
        public ILeaveApprovalsRepository LeaveApprovalsRepository
        {
            get
            {
                if (_leaveApprovalsRepository == null)
                {
                    _leaveApprovalsRepository = new LeaveApprovalsRepository(_connectionFactory);
                }
                return _leaveApprovalsRepository;
            }
        }
		#endregion

		#region IClaimRequestsRepository
		IClaimRequestsRepository _claimRequestsRepository;
		public IClaimRequestsRepository ClaimRequestsRepository
		{
			get
			{
				if (_claimRequestsRepository == null)
				{
					_claimRequestsRepository = new ClaimRequestsRepository(_connectionFactory);
				}
				return _claimRequestsRepository;
			}
		}
        #endregion

        #region IUserClaimRequestsRepository
        IUserClaimRequestsRepository _userClaimRequestsRepository;
        public IUserClaimRequestsRepository UserClaimRequestsRepository
        {
            get
            {
                if (_userClaimRequestsRepository == null)
                {
                    _userClaimRequestsRepository = new UserClaimRequestsRepository(_connectionFactory);
                }
                return _userClaimRequestsRepository;
            }
        }
        #endregion

        #region IOnDutyRequesteRepository
        IOnDutyRequesteRepository _onDutyRequesteRepository;
        public IOnDutyRequesteRepository OnDutyRequesteRepository
        {
            get
            {
                if (_onDutyRequesteRepository == null)
                {
                    _onDutyRequesteRepository = new OnDutyRequestRepository(_connectionFactory);
                }
                return _onDutyRequesteRepository;
            }
        }
        #endregion

        #region IDisposable Support
        void IUnitOfWork.Complete()
        {
            throw new NotImplementedException();
        }

        private bool disposedValue = false; // To detect redundant calls
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                disposedValue = true;
            }
        }
        void IDisposable.Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
