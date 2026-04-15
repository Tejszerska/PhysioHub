using PhysioHub.Data.Data.Scheduling;

namespace PhysioHub.Intranet.Models
{
    public class DashboardViewModel
    {
        public Stay? CurrentStay { get; set; }
        public int ActivePatientsCount { get; set; }
        public List<AppointmentSchedule> TodaysTreatments { get; set; } = new List<AppointmentSchedule>();
    }
}