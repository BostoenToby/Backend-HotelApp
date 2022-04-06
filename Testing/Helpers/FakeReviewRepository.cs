namespace Testing.Repositories;

public class FakeReviewRepository : IReviewRepository
{
    public static List<Review> _reviews = new();

    public Task<Review> AddReview(Review newReview)
    {
        _reviews.Add(newReview);
        return Task.FromResult(newReview);
    }

    public Task<string> DeleteReview(string Id)
    {
        try
        {
            Review reviewFound = _reviews.Find(c => c.Id == Id);
            _reviews.Remove(reviewFound);
            return Task.Run(() => "The review was removed successfully");
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            return Task.Run(() => "The review wasn't removed successfully");
        }
    }

    public Task<List<Review>> GetAllReviews()
    {
        return Task.FromResult(_reviews);
    }

    public Task<Review> GetReviewById(string Id)
    {
        Review reviewFound = _reviews.Find(c => c.Id == Id);
        return Task.FromResult(reviewFound);
    }

    public Task<List<Review>> GetReviewsByAuthor(string Author)
    {
        List<Review> reviewsFound = new List<Review>();
        foreach (Review review in _reviews)
        {
            if (review.Author == Author)
            {
                reviewsFound.Add(review);
            }
        }
        return Task.FromResult(reviewsFound);
    }

    public Task<Review> UpdateReview(Review review)
    {
        try
        {
            Review reviewFound = _reviews.Where(c => c.Id == review.Id).SingleOrDefault();
            if (reviewFound != null)
            {
                _reviews.Remove(reviewFound);
                _reviews.Add(review);
            }
            return Task.FromResult(review);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public static void AddFakeReview(Review fakeReview) => _reviews.Add(fakeReview);

}
