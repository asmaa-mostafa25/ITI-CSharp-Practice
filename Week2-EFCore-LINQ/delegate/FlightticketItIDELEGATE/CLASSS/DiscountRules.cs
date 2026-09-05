using System;
using System.Collections.Generic;
using System.Text;


namespace FlightticketItIDELEGATE.CLASSS
{
    public class DiscountRules
    {
        public static decimal DelayedFlightDiscount(FlightTicket ticket)
        {
            if(ticket.IsDelayed)
            {
                return ticket.BasePrice *  0.80m;
            }
            return ticket.BasePrice;
        }

        public decimal VipPassengerDiscount(FlightTicket ticket)
        {
            return ticket.BasePrice * 0.85m;
        }
    }
}
