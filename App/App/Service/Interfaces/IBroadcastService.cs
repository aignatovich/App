using System;
using System.Collections.Generic;

namespace App.Service.Interfaces
{
    public interface IBroadcastService
    {
        void Broadcast(IEnumerable<Int32> ids, string message);
    }
}