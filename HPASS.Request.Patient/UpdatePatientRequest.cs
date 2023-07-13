using HPASS.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPASS.Request.Patient
{
    public class UpdatePatientRequest
    {
        public Guid UpdatingPatientId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string NationalIdentifier { get; set; }
        public GenderTypes GenderType { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Weight { get; set; }
        public int Heigth { get; set; }
    }
}
