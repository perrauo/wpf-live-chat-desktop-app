using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFT585_TP3.Common
{
    public interface IDeepClonable<T>
    {
        T DeepClone();
    }
}
