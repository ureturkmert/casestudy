using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPASS.Request.Patient
{
    public class CreateMedicalHistoryRequest
    {
        public Guid PatientId { get; set; }

        public string Header { get; set; }
        public string Description { get; set; }
    }
}
