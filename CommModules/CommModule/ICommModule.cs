using CommModules.EventArgsClass;
using System;
using System.Threading.Tasks;

namespace CommModules
{
    public interface ICommModule
    {
        bool Connect();
        Task<bool> ConnectAsync();
        bool Disconnect();
        bool ConnectCheck();
        bool Send(byte[] sendData);
        void Receive();

        event EventHandler<ReceiveEventArg> ReceiveEvent;
    }
}
