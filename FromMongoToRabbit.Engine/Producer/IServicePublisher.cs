using System.Collections.Generic;

namespace FromMongoToRabbit.Engine.Producer
{
    public interface IServicePublisher
    {
        void RunService<T>(IEnumerable<T> listToSend);
    }
}