namespace YB_StaffingSupervisor.DataAccess.Entities
{
    public class ErrorLogModel
    {
        public string ApplicationType { get; set; }
        public string ExceptionMessage { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string ExceptionStackTrack { get; set; }
        public string UserId { get; set; }
    }
}
