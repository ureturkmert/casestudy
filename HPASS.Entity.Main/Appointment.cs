using HPASS.Entity.Base.Implementation;
using HPASS.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPASS.Entity.Main
{
    public class Appointment : BaseEntity
    {
        public DateTime Date { get; set; }
        public AppointmentStatusType Status { get; set; }
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }
        public Guid HealthcareProviderId { get; set; }
        public HealthcareProvider HealthcareProvider { get; set; }
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
        public Guid OperationZoneId { get; set; }
        public OperationZone OperationZone { get; set; }


        public ICollection<AppointmentReminderLog> AppointmentReminderLogs { get; set; }
    }
}
