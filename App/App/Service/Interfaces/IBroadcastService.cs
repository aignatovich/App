using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Service.Interfaces
{
    public interface IBroadcastService
    {
        void Broadcast(IEnumerable<Int32> ids, string message);
    }
}