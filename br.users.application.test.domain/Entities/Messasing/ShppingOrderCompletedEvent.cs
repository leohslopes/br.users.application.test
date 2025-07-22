using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace br.users.application.test.domain.Entities.Messasing
{
    public class ShppingOrderCompletedEvent
    {
        public ShppingOrderCompletedEvent(string trackingCode)
        {
                TrackingCode = trackingCode;
        }

        public string TrackingCode { get; set; }
    }
}
