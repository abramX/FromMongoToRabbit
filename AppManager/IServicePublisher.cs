using System.Collections.Generic;

namespace AppManager
{
    interface IServicePublisher
    {
        void RunService<T>(IEnumerable<T> listToSend);
    }
}