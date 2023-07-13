using HPASS.Entity.Base.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPASS.Entity.Main
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<Appointment> Appointments { get; set; }

    }
}
