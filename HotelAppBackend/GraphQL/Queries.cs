namespace Hotels.GraphQL.Queries;
public class Query
{
    // http://localhost:3000/graphql/
    // {hotels{name, ...}}
    public async Task<List<Hotel>> GetHotels([Service] IHotelService hotelService) => await hotelService.GetAllHotels();
    public async Task<List<Hotel>> GetHotelsByName([Service] IHotelService hotelService, string namePiece) => await hotelService.GetHotelsByNamePiece(namePiece);
    public async Task<Hotel> GetHotelById([Service] IHotelService hotelService, string id) => await hotelService.GetHotelById(id);
    public async Task<List<RoomType>> GetRoomTypes([Service] IHotelService hotelService) => await hotelService.GetAllRoomTypes();
    public async Task<List<RoomType>> GetRoomTypesByName([Service] IHotelService hotelService, string namePiece) => await hotelService.GetRoomTypesByNamePiece(namePiece);
    public async Task<RoomType> GetRoomTypeById([Service] IHotelService hotelService, string id) => await hotelService.GetRoomTypeById(id);
    public async Task<List<Reservation>> GetReservations([Service] IHotelService hotelService) => await hotelService.GetAllReservations();
    public async Task<List<Reservation>> GetReservationsByAuthor([Service] IHotelService hotelService, string name, string firstName) => await hotelService.GetReservationsByName(name, firstName);
    public async Task<Reservation> GetReservationById([Service] IHotelService hotelService, string id) => await hotelService.GetReservationById(id);
    public async Task<List<Review>> GetReviews([Service] IHotelService hotelService) => await hotelService.GetAllReviews();
    public async Task<List<Review>> GetReviewsByAuthor([Service] IHotelService hotelService, string author) => await hotelService.GetReviewsByAuthor(author);
    public async Task<Review> GetReviewById([Service] IHotelService hotelService, string id) => await hotelService.GetReviewById(id);
}