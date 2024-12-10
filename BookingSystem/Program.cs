// See https://aka.ms/new-console-template for more information

public class Concert
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public int AvailableSeats { get; set; }

    public Concert(string name, DateTime date, string location, int availableSeats)
    {
        Name = name;
        Date = date;
        Location = location;
        AvailableSeats = availableSeats;
    }
}
public class Ticket
{
    public Concert Concert { get; set; }
    public decimal Price { get; set; }
    public int SeatNumber { get; set; }

    public Ticket(Concert concert, decimal price, int seatNumber)
    {
        Concert = concert;
        Price = price;
        SeatNumber = seatNumber;
    }
}
public class BookingSystem
{
    private List<Concert> concerts = new List<Concert>();
    private List<Ticket> tickets = new List<Ticket>();
    
    public delegate void LowAvailabilityHandler(string message);
    public event LowAvailabilityHandler OnLowAvailability;

    public void AddConcert(Concert concert)
    {
        concerts.Add(concert);
    }

    public List<Concert> GetConcerts(Func<Concert, bool> filter)
    {
        return concerts.Where(filter).ToList();
    }

    public Ticket ReserveTicket(Concert concert)
    {
        if (concert.AvailableSeats > 0)
        {
            concert.AvailableSeats--;
            Ticket ticket = new Ticket(concert, 100m, concert.AvailableSeats);
            tickets.Add(ticket);
            
            if (concert.AvailableSeats <= 5)
                OnLowAvailability?.Invoke($"Niska dostępność biletów na koncert {concert.Name}!");

            return ticket;
        }
        else
        {
            Console.WriteLine("Brak dostępnych miejsc na ten koncert.");
            return null;
        }
    }

    public void DisplayReport()
    {
        var soldTickets = tickets.GroupBy(t => t.Concert.Name)
            .Select(group => new { Concert = group.Key, SoldTickets = group.Count() })
            .ToList();

        foreach (var report in soldTickets)
        {
            Console.WriteLine($"Koncert: {report.Concert}, Liczba sprzedanych biletów: {report.SoldTickets}");
        }
    }
}
public interface IConcert
{
    string Name { get; set; }
    DateTime Date { get; set; }
    string Location { get; set; }
    int AvailableSeats { get; set; }
    decimal Price { get; }
}

public class RegularConcert : IConcert
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public int AvailableSeats { get; set; }
    public decimal Price { get; set; }

    public RegularConcert(string name, DateTime date, string location, int availableSeats, decimal price)
    {
        Name = name;
        Date = date;
        Location = location;
        AvailableSeats = availableSeats;
        Price = price;
    }
}

public class VIPConcert : IConcert
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public int AvailableSeats { get; set; }
    public decimal Price { get; set; }
    public bool HasMeetAndGreet { get; set; }  // Możliwość spotkania z artystą

    public VIPConcert(string name, DateTime date, string location, int availableSeats, decimal price, bool hasMeetAndGreet)
    {
        Name = name;
        Date = date;
        Location = location;
        AvailableSeats = availableSeats;
        Price = price;
        HasMeetAndGreet = hasMeetAndGreet;
    }
}

public class OnlineConcert : IConcert
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public int AvailableSeats { get; set; }
    public decimal Price { get; set; }

    public OnlineConcert(string name, DateTime date, int availableSeats, decimal price)
    {
        Name = name;
        Date = date;
        Location = "Online";
        AvailableSeats = availableSeats;
        Price = price;
    }
}

public class PrivateConcert : IConcert
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public int AvailableSeats { get; set; }
    public decimal Price { get; set; }
    public string InvitationCode { get; set; }

    public PrivateConcert(string name, DateTime date, string location, int availableSeats, decimal price, string invitationCode)
    {
        Name = name;
        Date = date;
        Location = location;
        AvailableSeats = availableSeats;
        Price = price;
        InvitationCode = invitationCode;
    }
}