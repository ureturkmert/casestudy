using HPASS.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPASS.Request.Patient
{
    public class UpdateAppointmentStatusRequest
    {
        public Guid AppointmentId { get; set; }
        public AppointmentStatusType NewStatus { get; set; }
    }
}
