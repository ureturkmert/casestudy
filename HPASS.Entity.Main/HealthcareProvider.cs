using HPASS.Entity.Base.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPASS.Entity.Main
{
    public class HealthcareProvider : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<OperationZone> OperationZones { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<Appointment> Appointments { get; set; }


    }
}
