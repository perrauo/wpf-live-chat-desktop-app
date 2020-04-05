using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFT585_TP3.Client.NetworkFramework
{
    public class Result
    {
        public bool IsSuccess { get; set; }
    }

    public class Result<ResultType> : Result
    {
        public ResultType Value { get; set; }
    }
}
