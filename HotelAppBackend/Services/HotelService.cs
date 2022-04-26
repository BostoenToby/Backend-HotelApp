namespace Hotels.HotelService;

public interface IHotelService
{
    Task<Hotel> AddHotel(Hotel newHotel);
    Task<Reservation> AddReservation(Reservation newReservation);
    Task<Review> AddReview(Review newReview);
    Task<RoomType> AddRoomType(RoomType roomType);
    Task<string> DeleteHotel(string Id);
    Task<string> DeleteReservation(string Id);
    Task<string> DeleteReview(string Id);
    Task<string> DeleteRoomType(string Id);
    Task<List<Hotel>> GetAllHotels();
    Task<List<Reservation>> GetAllReservations();
    Task<List<Review>> GetAllReviews();
    Task<List<RoomType>> GetAllRoomTypes();
    Task<Hotel> GetHotelById(string Id);
    Task<List<Hotel>> GetHotelsByFilter(decimal PricePerNightMin, decimal PricePerNightMax, float StarRating);
    Task<List<Hotel>> GetHotelsByNamePiece(string NamePiece);
    Task<List<Hotel>> GetHotelsByRegion(string Region);
    Task<List<Hotel>> GetHotelsByRegionAndFilter(string Region, decimal PricePerNightMin, decimal PricePerNightMax, float StarRating);
    Task<Reservation> GetReservationById(string Id);
    Task<List<Reservation>> GetReservationsByFilter(string HotelName, DateTime DateOfReservation);
    Task<List<Reservation>> GetReservationsByName(string Name, string FirstName);
    Task<List<Reservation>> GetReservationsByRegion(string Region);
    Task<List<Reservation>> GetReservationsByRegionAndFilter(string Region, string HotelName, DateTime DateOfReservation);
    Task<Review> GetReviewById(string Id);
    Task<List<Review>> GetReviewsByAuthor(string Author);
    Task<List<Review>> GetReviewsByHotel(string hotelName);
    Task<RoomType> GetRoomTypeById(string Id);
    Task<List<RoomType>> GetRoomTypesByFilter(int NumberOfBeds, float SquareMeters, bool Television, bool Breakfast, bool Airco, bool Wifi, bool View);
    Task<List<RoomType>> GetRoomTypesByHotelNamePiece(string HotelName);
    Task<List<RoomType>> GetRoomTypesByNamePiece(string NamePiece);
    Task<List<RoomType>> GetRoomTypesByNamePieceAndFilter(string NamePiece, int NumberOfBeds, float SquareMeters, bool Television, bool Breakfast, bool Airco, bool Wifi, bool View);
    Task<Hotel> UpdateHotel(Hotel hotel);
    Task<Reservation> UpdateReservation(Reservation reservation);
    Task<Review> UpdateReview(Review review);
    Task<RoomType> UpdateRoomType(RoomType roomType);
}

public class HotelService : IHotelService
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly IReviewRepository _reviewRepository;

    public HotelService(IHotelRepository hotelRepository, IRoomTypeRepository roomTypeRepository, IReservationRepository reservationRepository, IReviewRepository reviewRepository)
    {
        _hotelRepository = hotelRepository;
        _roomTypeRepository = roomTypeRepository;
        _reservationRepository = reservationRepository;
        _reviewRepository = reviewRepository;
    }

    //Hotel
    public async Task<List<Hotel>> GetAllHotels() => await _hotelRepository.GetAllHotels();
    public async Task<List<Hotel>> GetHotelsByNamePiece(string NamePiece) => await _hotelRepository.GetHotelsByNamePiece(NamePiece);
    public async Task<List<Hotel>> GetHotelsByFilter(decimal PricePerNightMin, decimal PricePerNightMax, float StarRating) => await _hotelRepository.GetHotelsByFilter(PricePerNightMin, PricePerNightMax, StarRating);
    public async Task<List<Hotel>> GetHotelsByRegion(string Region) => await _hotelRepository.GetHotelsByRegion(Region);
    public async Task<List<Hotel>> GetHotelsByRegionAndFilter(string Region, decimal PricePerNightMin, decimal PricePerNightMax, float StarRating) => await _hotelRepository.GetHotelsByRegionAndFilter(Region, PricePerNightMin, PricePerNightMax, StarRating);
    public async Task<Hotel> GetHotelById(string Id) => await _hotelRepository.GetHotelById(Id);
    public async Task<Hotel> AddHotel(Hotel newHotel) => await _hotelRepository.AddHotel(newHotel);
    public async Task<Hotel> UpdateHotel(Hotel hotel) => await _hotelRepository.UpdateHotel(hotel);
    public async Task<string> DeleteHotel(string Id) => await _hotelRepository.DeleteHotel(Id);

    //RoomType
    public async Task<List<RoomType>> GetAllRoomTypes() => await _roomTypeRepository.GetAllRoomTypes();
    public async Task<List<RoomType>> GetRoomTypesByNamePiece(string NamePiece) => await _roomTypeRepository.GetRoomTypesByNamePiece(NamePiece);
    public async Task<List<RoomType>> GetRoomTypesByFilter(int NumberOfBeds, float SquareMeters, bool Television, bool Breakfast, bool Airco, bool Wifi, bool View) => await _roomTypeRepository.GetRoomTypesByFilter(NumberOfBeds, SquareMeters, Television, Breakfast, Airco, Wifi, View);
    public async Task<List<RoomType>> GetRoomTypesByNamePieceAndFilter(string NamePiece, int NumberOfBeds, float SquareMeters, bool Television, bool Breakfast, bool Airco, bool Wifi, bool View) => await _roomTypeRepository.GetRoomTypesByNamePieceAndFilter(NamePiece, NumberOfBeds, SquareMeters, Television, Breakfast, Airco, Wifi, View);
    public async Task<List<RoomType>> GetRoomTypesByHotelNamePiece(string HotelName) => await _roomTypeRepository.GetRoomTypesByHotelNamePiece(HotelName);
    public async Task<RoomType> GetRoomTypeById(string Id) => await _roomTypeRepository.GetRoomTypeById(Id);
    public async Task<RoomType> AddRoomType(RoomType roomType) => await _roomTypeRepository.AddRoomType(roomType);
    public async Task<RoomType> UpdateRoomType(RoomType roomType) => await _roomTypeRepository.UpdateRoomType(roomType);
    public async Task<string> DeleteRoomType(string Id) => await _roomTypeRepository.DeleteRoomType(Id);

    //Reservation
    public async Task<List<Reservation>> GetAllReservations() => await _reservationRepository.GetAllReservations();
    public async Task<List<Reservation>> GetReservationsByName(string Name, string FirstName) => await _reservationRepository.GetReservationsByName(Name, FirstName);
    public async Task<List<Reservation>> GetReservationsByRegion(string Region) => await _reservationRepository.GetReservationsByRegion(Region);
    public async Task<List<Reservation>> GetReservationsByFilter(string HotelName, DateTime DateOfReservation) => await _reservationRepository.GetReservationsByFilter(HotelName, DateOfReservation);
    public async Task<List<Reservation>> GetReservationsByRegionAndFilter(string Region, string HotelName, DateTime DateOfReservation) => await _reservationRepository.GetReservationsByRegionAndFilter(Region, HotelName, DateOfReservation);
    public async Task<Reservation> GetReservationById(string Id) => await _reservationRepository.GetReservationById(Id);
    public async Task<Reservation> AddReservation(Reservation newReservation) => await _reservationRepository.AddReservation(newReservation);
    public async Task<Reservation> UpdateReservation(Reservation reservation) => await _reservationRepository.UpdateReservation(reservation);
    public async Task<string> DeleteReservation(string Id) => await _reservationRepository.DeleteReservation(Id);

    //Review
    public async Task<List<Review>> GetAllReviews() => await _reviewRepository.GetAllReviews();
    public async Task<List<Review>> GetReviewsByAuthor(string Author) => await _reviewRepository.GetReviewsByAuthor(Author);
    public async Task<List<Review>> GetReviewsByHotel(string hotelName) => await _reviewRepository.GetReviewsByHotel(hotelName);
    public async Task<Review> GetReviewById(string Id) => await _reviewRepository.GetReviewById(Id);
    public async Task<Review> AddReview(Review newReview) => await _reviewRepository.AddReview(newReview);
    public async Task<Review> UpdateReview(Review review) => await _reviewRepository.UpdateReview(review);
    public async Task<string> DeleteReview(string Id) => await _reviewRepository.DeleteReview(Id);
}