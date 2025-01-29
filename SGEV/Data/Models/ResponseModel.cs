using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGEV.Data.Models
{
    internal class ResponseModel<T>
    {
        public string Message { get; set; }

        public bool Status { get; set; } = false;

        public string Data { get; set; } = string.Empty;

    }
}
