using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPASS.Request.Common
{
    public class PagingRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
