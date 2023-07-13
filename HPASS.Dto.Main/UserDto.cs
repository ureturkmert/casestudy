﻿
using HPASS.Dto.Base.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPASS.Dto.Main
{
    public class UserDto : BaseDto
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public HealthcareProviderDto HealthcareProvider { get; set; }
    }
}
