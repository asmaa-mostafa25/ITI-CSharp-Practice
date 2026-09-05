namespace FlightticketItIDELEGATE.CLASSS
{
    public class FlightTicket
    {
        public int TicketId { get; set; }
        public string PassengerName { get; set; }
        public decimal BasePrice { get; set; }
        public DateTime FlightTime { get; set; }
        public bool IsDelayed { get; set; }
    }

    // ===== Part 1: Custom Delegate =====
    public delegate decimal DiscountCalculator(FlightTicket ticket);

    // ===== Part 2: Custom Notification Delegate =====
    public delegate void NotificationSender(FlightTicket ticket);
}