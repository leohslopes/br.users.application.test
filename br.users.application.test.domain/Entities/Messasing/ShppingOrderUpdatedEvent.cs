using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace br.users.application.test.domain.Entities.Messasing
{
    public  class ShppingOrderUpdatedEvent
    {

        public ShppingOrderUpdatedEvent(string trackingCode, string contactEmail, string description)
        {
            TrackingCode = trackingCode;
            ContactEmail = contactEmail;
            Description = description;
        }

        public string TrackingCode { get; set; }

        public string ContactEmail { get; set; }

        public string Description { get; set; }
    }
}
