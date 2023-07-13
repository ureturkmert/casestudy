using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPASS.Response.Common
{
    public class PagingResponse<T> where T : class
    {

        public int NumberOfTotalEntities { get; set; }

        public IList<T> Entities { get; set; }

        public PagingResponse()
        {
            this.Entities = new List<T>();
        }
    }

}
