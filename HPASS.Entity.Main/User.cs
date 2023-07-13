using HPASS.Entity.Base.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPASS.Entity.Main
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Guid HealthcareProviderId { get; set; }
        public HealthcareProvider HealthcareProvider { get; set; }
    }
}
