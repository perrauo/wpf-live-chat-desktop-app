using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public enum Status
    {
        ConnectionError,
        NonExistentItem,
        Success
    }

    public class Result<T>
    {
        public Status Status { get; set; }
        public T Return { get; set; } 
    }
}
