using HPASS.Dto.Base.Implementation;
using HPASS.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPASS.Dto.Main
{
    public class AppointmentDto : BaseDto
    {
        public DateTime Date { get; set; }
        public AppointmentStatusType Status { get; set; }
        public PatientDto Patient { get; set; }
        public HealthcareProviderDto HealthcareProvider { get; set; }
        public DoctorDto Doctor { get; set; }
        public DepartmentDto Department { get; set; }
        public OperationZoneDto OperationZone { get; set; }
    }
}
