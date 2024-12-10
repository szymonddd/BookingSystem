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