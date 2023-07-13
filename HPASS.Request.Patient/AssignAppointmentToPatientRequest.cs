using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPASS.Request.Patient
{
    public class AssignAppointmentToPatientRequest
    {
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid OperationZoneId { get; set; }
        public DateTime Date { get; set; }
    }
}
