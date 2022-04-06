namespace Hotels.Repositories;

public interface IReservationRepository
{
    Task<Reservation> AddReservation(Reservation newReservation);
    Task<string> DeleteReservation(string Id);
    Task<List<Reservation>> GetAllReservations();
    Task<Reservation> GetReservationById(string Id);
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

    //GET
    public async Task<List<Reservation>> GetAllReservations() => await _context.ReservationCollection.Find<Reservation>(_ => true).ToListAsync();
    public async Task<List<Reservation>> GetReservationsByName(string Name, string FirstName) => await _context.ReservationCollection.Find<Reservation>(c => (c.Name == Name) & (c.FirstName == FirstName)).ToListAsync();
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
            var update = Builders<Reservation>.Update.Set("Name", reservation.Name).Set("FirstName", reservation.FirstName).Set("BirthDate", reservation.BirthDate).Set("EMail", reservation.EMail).Set("Hotel", reservation.Hotel).Set("NumberOfRooms", reservation.NumberOfRooms).Set("DateOfReservation", reservation.DateOfReservation).Set("Review", reservation.Review).Set("TotalPrice", reservation.TotalPrice);
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