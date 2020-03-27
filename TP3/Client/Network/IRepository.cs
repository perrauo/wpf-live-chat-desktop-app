using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFT585_TP3.Client.Network
{
    public interface IRepository<T>
    {
        Connection Connection { get;  }

        T Create(T obj);
        T Retrieve(T obj);
        T Update(T obj);
        T Delete(T obj);
        T Exists(T obj);
    }
}
