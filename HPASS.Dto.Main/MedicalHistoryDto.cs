using HPASS.Dto.Base.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPASS.Dto.Main
{
    public class MedicalHistoryDto : BaseDto
    {
        public string Header { get; set; }
        public string Description { get; set; }
    }
}
