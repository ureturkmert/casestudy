using HPASS.Entity.Base.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPASS.Entity.Main
{
    public class Doctor : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Title { get; set; }

        public Guid HealthcareProviderId { get; set; }
        public HealthcareProvider HealthcareProvider { get; set; }

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Appointment> Appointments { get; set; }

    }
}
