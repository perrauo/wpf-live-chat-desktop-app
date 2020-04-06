using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IFT585_TP3.Client.NetworkFramework
{
    public class Result
    {
        public bool IsSuccess
        {
            get
            {
                return 200 <= (int)StatusCode && (int)StatusCode < 300;
            }
        }

        public HttpStatusCode StatusCode { get; set; }
    }

    public class Result<ResultType> : Result
    {
        public ResultType Value { get; set; }
    }
}
    