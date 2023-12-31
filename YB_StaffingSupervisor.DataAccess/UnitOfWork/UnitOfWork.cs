﻿
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

        #region IDesignationRepository
        IDesignationRepository _designationRepository;
        public IDesignationRepository DesignationRepository
        {
            get
            {
                if (_designationRepository == null)
                {
                    _designationRepository = new DesignationRepository(_connectionFactory);
                }
                return _designationRepository;
            }
        }
        #endregion

        #region  IUserProfileRepository 
        public IUserProfileRepository _userProfileRepository;
        public IUserProfileRepository UserProfileRepository
        {
            get
            {
                if (_userProfileRepository == null)
                {
                    _userProfileRepository = new UserProfileRepository(_connectionFactory);
                }
                return _userProfileRepository;
            }
        }
        #endregion

        #region  IAttendanceRepository 
        public IAttendanceRepository _attendanceRepository;
        public IAttendanceRepository AttendanceRepository
        {
            get
            {
                if (_attendanceRepository == null)
                {
                    _attendanceRepository = new AttendanceRepository(_connectionFactory);
                }
                return _attendanceRepository;
            }
        }
        #endregion

        #region  IAttendanceCorrectionRepository 
        public IAttendanceCorrectionRepository _attendanceCorrectionRepository;
        public IAttendanceCorrectionRepository AttendanceCorrectionRepository
        {
            get
            {
                if (_attendanceCorrectionRepository == null)
                {
                    _attendanceCorrectionRepository = new AttendanceCorrectionRepository(_connectionFactory);
                }
                return _attendanceCorrectionRepository;
            }
        }
        #endregion

        #region  IOnDutyRepository 
        public IOnDutyRepository _onDutyRepository;
        public IOnDutyRepository OnDutyRepository
        {
            get
            {
                if (_onDutyRepository == null)
                {
                    _onDutyRepository = new OnDutyRepository(_connectionFactory);
                }
                return _onDutyRepository;
            }
        }
		#endregion


		#region ILeaveRepository
		ILeaveRepository _leaveRepository;
		public ILeaveRepository LeaveRepository
		{
			get
			{
				if (_leaveRepository == null)
				{
					_leaveRepository = new LeaveRepository(_connectionFactory);
				}
				return _leaveRepository;
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

        #region  IAttendanceMeetingMapRepository 
        public IAttendanceMeetingMapRepository _attendanceMeetingMapRepository;
        public IAttendanceMeetingMapRepository AttendanceMeetingMapRepository
        {
            get
            {
                if (_attendanceMeetingMapRepository == null)
                {
                    _attendanceMeetingMapRepository = new AttendanceMeetingMapRepository(_connectionFactory);
                }
                return _attendanceMeetingMapRepository;
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
