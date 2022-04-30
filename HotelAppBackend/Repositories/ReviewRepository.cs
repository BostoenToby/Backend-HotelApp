namespace Hotels.Repositories;

public interface IReviewRepository
{
    Task<Review> AddReview(Review newReview);
    Task<string> DeleteReview(string Id);
    Task<List<Review>> GetAllReviews();
    Task<Review> GetReviewById(string Id);
    Task<List<Review>> GetReviewsByAuthor(string Author);
    Task<List<Review>> GetReviewsByHotel(string hotelName);
    Task<Review> UpdateReview(Review review);
}

public class ReviewRepository : IReviewRepository
{
    private readonly IMongoContext _context;
    public ReviewRepository(IMongoContext context)
    {
        _context = context;
    }

    //GET
    public async Task<List<Review>> GetAllReviews() => await _context.ReviewsCollection.Find<Review>(_ => true).ToListAsync();
    public async Task<List<Review>> GetReviewsByAuthor(string Author) => await _context.ReviewsCollection.Find<Review>(c => c.Author == Author).ToListAsync();
    public async Task<List<Review>> GetReviewsByHotel(string hotelName) => await _context.ReviewsCollection.Find<Review>(c => c.HotelName.Contains(hotelName)).ToListAsync();
    public async Task<Review> GetReviewById(string Id) => await _context.ReviewsCollection.Find<Review>(c => c.Id == Id).FirstOrDefaultAsync();

    //POST
    public async Task<Review> AddReview(Review newReview)
    {
        await _context.ReviewsCollection.InsertOneAsync(newReview);
        return newReview;
    }

    //PUT
    public async Task<Review> UpdateReview(Review review)
    {
        try
        {
            var filter = Builders<Review>.Filter.Eq("Id", review.Id);
            var update = Builders<Review>.Update.Set("Author", review.Author).Set("StarRating", review.StarRating).Set("Image", review.Image).Set("ReviewDescription", review.ReviewDescription);
            var result = await _context.ReviewsCollection.UpdateOneAsync(filter, update);
            return await GetReviewById(review.Id);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    //DELETE
    public async Task<string> DeleteReview(string Id)
    {
        try
        {
            var filter = Builders<Review>.Filter.Eq("Id", Id);
            var result = await _context.ReviewsCollection.DeleteOneAsync(filter);
            return "The review has succesfully been removed";
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            return "The review hasn't been removed";
        }
    }
}