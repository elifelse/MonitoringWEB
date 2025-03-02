namespace MonitoringApp.WebUI.Models
{
    public class DashboardViewModel
    {
        public List<ServiceHealthLog> ServiceHealthLogs { get; set; }
        public List<ErrorLog> Errors { get; set; }
        public List<JobStatus> Jobs { get; set; }
    }
}
