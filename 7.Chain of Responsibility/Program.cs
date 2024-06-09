using System;

abstract class TicketHandler
{
    protected TicketHandler nextHandler;

    public TicketHandler SetNext(TicketHandler handler)
    {
        nextHandler = handler;
        return handler;
    }

    public virtual void Handle(Ticket ticket)
    {
        if (nextHandler != null)
        {
            nextHandler.Handle(ticket);
        }
    }
}

class TechnicalSupportHandler : TicketHandler
{
    public override void Handle(Ticket ticket)
    {
        if (ticket.Type == "technical")
        {
            Console.WriteLine("Handling technical ticket: " + ticket.Description);
        }
        else
        {
            base.Handle(ticket);
        }
    }
}

class BillingSupportHandler : TicketHandler
{
    public override void Handle(Ticket ticket)
    {
        if (ticket.Type == "billing")
        {
            Console.WriteLine("Handling billing ticket: " + ticket.Description);
        }
        else
        {
            base.Handle(ticket);
        }
    }
}

class GeneralSupportHandler : TicketHandler
{
    public override void Handle(Ticket ticket)
    {
        if (ticket.Type == "general")
        {
            Console.WriteLine("Handling general ticket: " + ticket.Description);
        }
        else
        {
            base.Handle(ticket);
        }
    }
}

class Ticket
{
    public string Type { get; }
    public string Description { get; }

    public Ticket(string type, string description)
    {
        Type = type;
        Description = description;
    }
}

class Client
{
    public Ticket GenerateTicket(string type, string description)
    {
        return new Ticket(type, description);
    }

    public void ProcessTicket(Ticket ticket, TicketHandler handler)
    {
        handler.Handle(ticket);
    }
}

class Program
{
    static void Main()
    {
        var techHandler = new TechnicalSupportHandler();
        var billingHandler = new BillingSupportHandler();
        var generalHandler = new GeneralSupportHandler();

        techHandler.SetNext(billingHandler).SetNext(generalHandler);

        var client = new Client();

        var ticket1 = client.GenerateTicket("technical", "Unable to connect to the internet.");
        var ticket2 = client.GenerateTicket("billing", "Question about my invoice.");
        var ticket3 = client.GenerateTicket("general", "General inquiry about services.");

        client.ProcessTicket(ticket1, techHandler);
        client.ProcessTicket(ticket2, techHandler);
        client.ProcessTicket(ticket3, techHandler);
    }
}
