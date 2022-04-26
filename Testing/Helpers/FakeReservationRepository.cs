namespace Testing.Repositories;

public class FakeReservationRepository : IReservationRepository
{
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
    public static List<Reservation> _reservations = new();
    public Task<Reservation> AddReservation(Reservation newReservation)
    {
        _reservations.Add(newReservation);
        return Task.FromResult(newReservation);
    }

    public Task<List<Reservation>> GetAllReservations()
    {
        return Task.FromResult(_reservations);
    }

    public Task<Reservation> GetReservationById(string Id)
    {
        Reservation reservationFound = _reservations.Find(c => c.Id == Id);
        return Task.FromResult(reservationFound);
    }

    public Task<List<Reservation>> GetReservationsByName(string Name, string FirstName)
    {
        List<Reservation> reservationsFound = new List<Reservation>();
        foreach(Reservation reservation in _reservations){
            if(reservation.Name == Name && reservation.FirstName == FirstName){
                reservationsFound.Add(reservation);
            }
        }
        return Task.FromResult(reservationsFound);
    }

        public Task<List<Reservation>> GetReservationsByFilter(string HotelName, DateTime DateOfReservation)
    {
        List<Reservation> reservationsFound = new List<Reservation>();
        foreach(Reservation reservation in _reservations)
        if(reservation.Hotel.Name.Contains(HotelName)) //if not filled in = ""
        if(DateOfReservation.Month == reservation.DateOfReservation.Month && DateOfReservation.Day == reservation.DateOfReservation.Day && DateOfReservation.Year == reservation.DateOfReservation.Year) //if not filled in = 1/1/1900
        reservationsFound.Add(reservation);
        return Task.FromResult(reservationsFound);
    }

    public Task<List<Reservation>> GetReservationsByRegion(string Region)
    {
        List<Reservation> reservationsFound = new List<Reservation>();
        if(Provinces.Contains(Region)){
            foreach(Reservation reservation in _reservations)
            if(reservation.Hotel.Province == Region)
            reservationsFound.Add(reservation);
        }
        else{
            foreach(Reservation reservation in _reservations)
            if(reservation.Hotel.City == Region)
            reservationsFound.Add(reservation);
        }
        return Task.FromResult(reservationsFound);
    }

    public Task<List<Reservation>> GetReservationsByRegionAndFilter(string Region, string HotelName, DateTime DateOfReservation)
    {
        List<Reservation> reservationsFoundFilter = new List<Reservation>();
        List<Reservation> reservationsFound = new List<Reservation>();
        foreach(Reservation reservation in _reservations)
        if(reservation.Hotel.Name.Contains(HotelName)) //if not filled in = ""
        if(DateOfReservation.Month == reservation.DateOfReservation.Month && DateOfReservation.Day == reservation.DateOfReservation.Day && DateOfReservation.Year == reservation.DateOfReservation.Year) //if not filled in = 1/1/1900
        reservationsFoundFilter.Add(reservation);

        foreach(Reservation reservation in reservationsFoundFilter)
        if(Provinces.Contains(Region)){
            if(reservation.Hotel.Province == Region)
            reservationsFound.Add(reservation);
        }
        else{
            if(reservation.Hotel.City == Region)
            reservationsFound.Add(reservation);
        }
        return Task.FromResult(reservationsFound);
    }

    public static void AddFakeReservation(Reservation fakeReservation) => _reservations.Add(fakeReservation);
    public Task<Reservation> UpdateReservation(Reservation reservation)
    {
        try
        {
            Reservation reservationFound = _reservations.Where(c => c.Id == reservation.Id).SingleOrDefault();
            if(reservationFound != null){
                _reservations.Remove(reservationFound);
                _reservations.Add(reservationFound);
            }
            return Task.FromResult(reservationFound);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

        public Task<string> DeleteReservation(string Id)
    {
        try
        {
            Reservation reservationFound = _reservations.Find(c => c.Id == Id);
            _reservations.Remove(reservationFound);
            return Task.Run(() => "The reservation was removed successfully");
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            return Task.Run(() => "The reservation wasn't removed successfully");
        }
    }
}
