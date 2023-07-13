using HPASS.Entity.Base.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPASS.Entity.Main
{
    public class MedicalHistory : BaseEntity
    {
        public string Header { get; set; }
        public string Description { get; set; }

        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
