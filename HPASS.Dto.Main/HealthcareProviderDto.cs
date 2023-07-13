using HPASS.Dto.Base.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPASS.Dto.Main
{
    public class HealthcareProviderDto : BaseDto
    {
        public string Name { get; set; }
        public IEnumerable<OperationZoneDto> OperationZones { get; set; }
        public IEnumerable<DoctorDto> Doctors { get; set; }
    }
}
