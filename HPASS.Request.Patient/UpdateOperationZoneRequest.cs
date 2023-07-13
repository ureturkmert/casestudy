using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPASS.Request.Patient
{
    public class UpdateOperationZoneRequest
    {
        public Guid UpdatingOperationZoneId { get; set; }
        public string Name { get; set; }
    }
}
