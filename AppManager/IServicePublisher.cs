using System.Collections.Generic;

namespace AppManager
{
    public interface IServicePublisher
    {
        void RunService<T>(IEnumerable<T> listToSend);
    }
}