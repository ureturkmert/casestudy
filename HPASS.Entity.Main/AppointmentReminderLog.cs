using HPASS.Entity.Base.Implementation;
using HPASS.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPASS.Entity.Main
{
    public class AppointmentReminderLog : BaseEntity
    {
        public NotificationTransmissionType TransmissionType { get; set; }

        public Guid AppointmentId { get; set; }
        public Appointment Appointment { get; set; }

    }
}
