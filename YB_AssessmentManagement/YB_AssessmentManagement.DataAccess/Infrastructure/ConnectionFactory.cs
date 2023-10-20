using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace YB_AssessmentManagement.DataAccess.Infrastructure
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;
        private readonly string _userlogconnectionString;
        private readonly string _errorlogconnectionString;
        private readonly string _smsemaillogconnectionString;
        private readonly string _whatsappconnectionString;
        public ConnectionFactory(IConfiguration iconfiguration)
        {
            //Default Connection
            _connectionString = iconfiguration.GetConnectionString("DefaultConnection");
            //User Log Connection
            _userlogconnectionString = iconfiguration.GetConnectionString("LogConnection");
            //Error Log Connection
            _errorlogconnectionString = iconfiguration.GetConnectionString("ErrorLogConnection");
            //Sms Email Log connection 
            _smsemaillogconnectionString = iconfiguration.GetConnectionString("SMSEmailLogConnection");
            //Whatsapp DB connection 
            _whatsappconnectionString = iconfiguration.GetConnectionString("WhatsappDBConnection");
        }

        public DAL GetDAL
        {
            get
            {
                SqlConnection conn = new SqlConnection
                {
                    ConnectionString = _connectionString
                };
                DAL dal = new DAL(conn);
                return dal;
            }
        }

        public DAL GetUserLogDAL
        {
            get
            {
                SqlConnection conn = new SqlConnection
                {
                    ConnectionString = _userlogconnectionString
                };
                DAL dal = new DAL(conn);
                return dal;
            }
        }

        public DAL GetErrorLogDAL
        {
            get
            {
                SqlConnection conn = new SqlConnection
                {
                    ConnectionString = _errorlogconnectionString
                };
                DAL dal = new DAL(conn);
                return dal;
            }
        }
        public DAL GetSMSEmailLogDAL
        {
            get
            {
                SqlConnection conn = new SqlConnection
                {
                    ConnectionString = _smsemaillogconnectionString
                };
                DAL dal = new DAL(conn);
                return dal;
            }
        }
        public DAL GetWhatsappDAL
        {
            get
            {
                SqlConnection conn = new SqlConnection
                {
                    ConnectionString = _whatsappconnectionString
                };
                DAL dal = new DAL(conn);
                return dal;
            }
        }

        #region IDisposable Support
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
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
