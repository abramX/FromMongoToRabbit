using System.Collections.Generic;

namespace RabbitSender
{
    public interface IServicePublisher
    {
        void RunService<T>(IEnumerable<T> listToSend);
    }
}