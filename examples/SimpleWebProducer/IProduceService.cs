using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebProducer
{
    public interface IProduceService : IDisposable
    {
        void Produce();
    }
}
