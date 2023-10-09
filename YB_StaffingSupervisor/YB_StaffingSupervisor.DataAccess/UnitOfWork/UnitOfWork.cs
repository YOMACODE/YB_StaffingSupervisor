
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
