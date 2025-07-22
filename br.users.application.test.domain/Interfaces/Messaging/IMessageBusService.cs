using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace br.users.application.test.domain.Interfaces.Messaging
{
    public interface IMessageBusService
    {
        void PublishMessage(object data, string routingKey);
    }
}
