using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.CrossCutting.Interfaces
{
    public interface IConsumer
    {
        void Consume<T>(string targetQueue, string rabbitConnection, Action<T> callback);
    }
}
