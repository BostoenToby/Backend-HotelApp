namespace Hotels.Repositories;

public interface IReservationRepository
{
    Task<Reservation> AddReservation(Reservation newReservation);
    Task<string> DeleteReservation(string Id);
    Task<List<Reservation>> GetAllReservations();
    Task<Reservation> GetReservationById(string Id);
    Task<List<Reservation>> GetReservationsByFilter(string HotelName, DateTime DateOfReservation);
    Task<List<Reservation>> GetReservationsByName(string Name, string FirstName);
    Task<List<Reservation>> GetReservationsByRegion(string Region);
    Task<List<Reservation>> GetReservationsByRegionAndFilter(string Region, string HotelName, DateTime DateOfReservation);
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
    public async Task<List<Reservation>> GetReservationsByName(string Name, string FirstName) => await _context.ReservationCollection.Find<Reservation>(c => (c.Name == Name) & (c.FirstName == FirstName)).ToListAsync();
    public async Task<List<Reservation>> GetReservationsByRegion(string Region)
    {
        List<Reservation> chosenReservations = new List<Reservation>();
        if(Provinces.Contains(Region))
        chosenReservations = await _context.ReservationCollection.Find(c => c.Hotel.Province == Region).ToListAsync();
        else
        chosenReservations = await _context.ReservationCollection.Find(c => c.Hotel.City == Region).ToListAsync();
        return chosenReservations;
    }
    public async Task<List<Reservation>> GetReservationsByFilter(string HotelName, DateTime DateOfReservation)
    {
        List<Reservation> allReservations = await GetAllReservations();
        List<Reservation> chosenReservations = new List<Reservation>();
        foreach(Reservation reservation in allReservations)
        if(reservation.Hotel.Name.Contains(HotelName)) //if not filled in = ""
        if(DateOfReservation.Month == reservation.DateOfReservation.Month && DateOfReservation.Day == reservation.DateOfReservation.Day && DateOfReservation.Year == reservation.DateOfReservation.Year) //if not filled in = 1/1/1900
        chosenReservations.Add(reservation);
        return chosenReservations;
    }
    public async Task<List<Reservation>> GetReservationsByRegionAndFilter(string Region, string HotelName, DateTime DateOfReservation)
    {
        List<Reservation> chosenReservationsRegion = new List<Reservation>();
        List<Reservation> chosenReservations = new List<Reservation>();
        if(Provinces.Contains(Region))
        chosenReservationsRegion = await _context.ReservationCollection.Find(c => c.Hotel.Province == Region).ToListAsync();
        else
        chosenReservationsRegion = await _context.ReservationCollection.Find(c => c.Hotel.City == Region).ToListAsync();

        foreach(Reservation reservation in chosenReservationsRegion)
        if(reservation.Hotel.Name.Contains(HotelName))
        if(DateOfReservation.Month == reservation.DateOfReservation.Month && DateOfReservation.Day == reservation.DateOfReservation.Day && DateOfReservation.Year == reservation.DateOfReservation.Year)
        chosenReservations.Add(reservation);
        return chosenReservations;

    }
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
            var update = Builders<Reservation>.Update.Set("Name", reservation.Name).Set("FirstName", reservation.FirstName).Set("BirthDate", reservation.BirthDate).Set("EMail", reservation.EMail).Set("Hotel", reservation.Hotel).Set("DateOfReservation", reservation.DateOfReservation).Set("Review", reservation.Review).Set("TotalPrice", reservation.TotalPrice).Set("RoomType", reservation.RoomType);
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