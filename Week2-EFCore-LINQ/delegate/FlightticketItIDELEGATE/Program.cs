using FlightticketItIDELEGATE.CLASSS;
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // ==========================================
        // PART 1 - Create Flight Tickets
        // ==========================================

        List<FlightTicket> flightTickets = new List<FlightTicket>
        {
            new FlightTicket
            {
                TicketId = 1,
                PassengerName = "Ahmed",
                BasePrice = 800,
                FlightTime = new DateTime(2026, 9, 5, 9, 30, 0),
                IsDelayed = true
            },

            new FlightTicket
            {
                TicketId = 2,
                PassengerName = "Mona",
                BasePrice = 1200,
                FlightTime = new DateTime(2026, 9, 5, 13, 00, 0),
                IsDelayed = false
            },

            new FlightTicket
            {
                TicketId = 3,
                PassengerName = "Omar",
                BasePrice = 1500,
                FlightTime = new DateTime(2026, 9, 5, 10, 00, 0),
                IsDelayed = false
            },

            new FlightTicket
            {
                TicketId = 4,
                PassengerName = "Sara",
                BasePrice = 950,
                FlightTime = new DateTime(2026, 9, 5, 16, 30, 0),
                IsDelayed = true
            },

            new FlightTicket
            {
                TicketId = 5,
                PassengerName = "Youssef",
                BasePrice = 2000,
                FlightTime = new DateTime(2026, 9, 5, 11, 00, 0),
                IsDelayed = false
            }
        };


        // ==========================================
        // PART 1 - Custom Delegate
        // ==========================================

        // Static method
        ProcessTickets(
            flightTickets,
            DiscountRules.DelayedFlightDiscount
        );


        // Instance method
        DiscountRules discountRules = new DiscountRules();

        ProcessTickets(
            flightTickets,
            discountRules.VipPassengerDiscount
        );


        // ==========================================
        // PART 1 - Anonymous Method
        // Holiday Special 10%
        // ==========================================

        ProcessTickets(
            flightTickets,
            delegate (FlightTicket t)
            {
                return t.BasePrice * 0.90m;
            }
        );


        // ==========================================
        // PART 1 - Lambda
        // Morning Flight Discount 5%
        // ==========================================

        ProcessTickets(
            flightTickets,
            t => t.FlightTime.Hour < 12
                ? t.BasePrice * 0.95m
                : t.BasePrice
        );


        // ==========================================
        // PART 1 - Lambda
        // Premium Ticket Discount $50
        // ==========================================

        ProcessTickets(
            flightTickets,
            t => t.BasePrice > 1000
                ? t.BasePrice - 50
                : t.BasePrice
        );


        // ==========================================
        // PART 2 - Action
        // ==========================================

        BroadcastNotification(
            flightTickets[0],

            t => Console.WriteLine(
                $"[System Log] Processing ticket for {t.PassengerName}."
            )
        );


        // ==========================================
        // PART 2 - Predicate
        // ==========================================

        List<FlightTicket> delayedTickets =
            FilterTickets(
                flightTickets,
                t => t.IsDelayed
            );

        Console.WriteLine("\nDelayed Tickets:");

        foreach (var ticket in delayedTickets)
        {
            Console.WriteLine(ticket.PassengerName);
        }


        // ==========================================
        // PART 2 - Func
        // Replace Custom Delegate
        // ==========================================

        ProcessTicketsWithFunc(
            flightTickets,
            DiscountRules.DelayedFlightDiscount
        );


        // ==========================================
        // PART 2 - Func
        // Ticket Summaries
        // ==========================================

        List<string> summaries =
            GetTicketSummaries(
                flightTickets,
                t => $"{t.TicketId} - {t.PassengerName}"
            );

        Console.WriteLine("\nTicket Summaries:");

        foreach (var summary in summaries)
        {
            Console.WriteLine(summary);
        }
    }


    // ==================================================
    // PART 1
    // Custom Delegate Processing
    // ==================================================

    public static void ProcessTickets(
        List<FlightTicket> tickets,
        DiscountCalculator discount)
    {
        Console.WriteLine("\n--- Custom Delegate ---");

        foreach (var item in tickets)
        {
            decimal finalPrice = discount(item);

            Console.WriteLine(
                $"Passenger: {item.PassengerName}, " +
                $"Original Price: {item.BasePrice}, " +
                $"Final Price: {finalPrice}"
            );
        }
    }


    // ==================================================
    // PART 2
    // ACTION
    // ==================================================

    public static void BroadcastNotification(
        FlightTicket ticket,
        Action<FlightTicket> notificationAction)
    {
        notificationAction(ticket);
    }


    // ==================================================
    // PART 2
    // PREDICATE
    // ==================================================

    public static List<FlightTicket> FilterTickets(
        List<FlightTicket> tickets,
        Predicate<FlightTicket> condition)
    {
        List<FlightTicket> result = new List<FlightTicket>();

        foreach (var ticket in tickets)
        {
            if (condition(ticket))
            {
                result.Add(ticket);
            }
        }

        return result;
    }


    // ==================================================
    // PART 2
    // FUNC
    // ==================================================

    public static void ProcessTicketsWithFunc(
        List<FlightTicket> tickets,
        Func<FlightTicket, decimal> discount)
    {
        Console.WriteLine("\n--- Func Delegate ---");

        foreach (var item in tickets)
        {
            decimal finalPrice = discount(item);

            Console.WriteLine(
                $"Passenger: {item.PassengerName}, " +
                $"Original Price: {item.BasePrice}, " +
                $"Final Price: {finalPrice}"
            );
        }
    }


    // ==================================================
    // PART 2
    // FUNC - Ticket Summaries
    // ==================================================

    public static List<string> GetTicketSummaries(
        List<FlightTicket> tickets,
        Func<FlightTicket, string> formatter)
    {
        List<string> summaries = new List<string>();

        foreach (var ticket in tickets)
        {
            summaries.Add(formatter(ticket));
        }

        return summaries;
    }
}