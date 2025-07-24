using br.users.application.test.domain.Entities.Messasing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace br.users.application.test.domain.Interfaces.Messaging
{
    public interface IMessageBusService
    {
        void PublishMessage(UserDTO dto);
    }
}
