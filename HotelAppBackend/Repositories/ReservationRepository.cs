namespace Hotels.Repositories;

public interface IReservationRepository
{
    Task<Reservation> AddReservation(Reservation newReservation);
    Task<string> DeleteReservation(string Id);
    Task<List<Reservation>> GetAllReservations();
    Task<Reservation> GetReservationById(string Id);
    Task<List<Reservation>> GetReservationsByFilter(string HotelName, string IncheckDate, string OutcheckDate);
    Task<List<Reservation>> GetReservationsByMail(string Mail);
    Task<List<Reservation>> GetReservationsByName(string Name, string FirstName);
    Task<Reservation> UpdateReservation(Reservation reservation);
}

public class ReservationRepository : IReservationRepository
{
    private readonly IMongoContext _context;
    public ReservationRepository(IMongoContext context)
    {
        _context = context;
    }

    List<string> Provinces = new List<string>(){
        "West-Vlaanderen",
        "Oost-Vlaanderen",
        "Antwerpen",
        "Henegouwen",
        "Limburg",
        "Luik",
        "Luxemburg",
        "Namen",
        "Vlaams-Brabant",
        "Waals-Brabant"
    };

    //GET
    public async Task<List<Reservation>> GetAllReservations() => await _context.ReservationCollection.Find<Reservation>(_ => true).ToListAsync();
    public async Task<List<Reservation>> GetReservationsByName(string Name, string FirstName) => await _context.ReservationCollection.Find<Reservation>(c => (c.LastName == Name) & (c.FirstName == FirstName)).ToListAsync();
    public async Task<List<Reservation>> GetReservationsByFilter(string HotelName, string IncheckDate, string OutcheckDate)
    {
        List<Reservation> allReservations = await GetAllReservations();
        List<Reservation> chosenReservations = new List<Reservation>();
        foreach (Reservation reservation in allReservations)
            if (reservation.HotelName.Contains(HotelName))
                if (IncheckDate == reservation.IncheckDate && OutcheckDate == reservation.OutcheckDate)
                    chosenReservations.Add(reservation);
        return chosenReservations;
    }
    public async Task<List<Reservation>> GetReservationsByMail(string Mail) => await _context.ReservationCollection.Find<Reservation>(c => c.Mail == Mail).ToListAsync();
    public async Task<Reservation> GetReservationById(string Id) => await _context.ReservationCollection.Find<Reservation>(c => c.Id == Id).FirstOrDefaultAsync();
    //POST
    public async Task<Reservation> AddReservation(Reservation newReservation)
    {
        await _context.ReservationCollection.InsertOneAsync(newReservation);
        return newReservation;
    }

    //PUT
    public async Task<Reservation> UpdateReservation(Reservation reservation)
    {
        try
        {
            var filter = Builders<Reservation>.Filter.Eq("Id", reservation.Id);
            var update = Builders<Reservation>.Update.Set("LastName", reservation.LastName).Set("FirstName", reservation.FirstName).Set("Mail", reservation.Mail).Set("HotelName", reservation.HotelName).Set("IncheckDate", reservation.IncheckDate).Set("OutcheckDate", reservation.OutcheckDate).Set("Price", reservation.Price).Set("RoomTypeName", reservation.RoomTypeName);
            var result = await _context.ReservationCollection.UpdateOneAsync(filter, update);
            return await GetReservationById(reservation.Id);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    //DELETE
    public async Task<string> DeleteReservation(string Id)
    {
        try
        {
            var filter = Builders<Reservation>.Filter.Eq("Id", Id);
            var result = await _context.ReservationCollection.DeleteOneAsync(filter);
            return "The reservation has succesfully been removed";
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            return "The reservation hasn't been removed";
        }
    }
}