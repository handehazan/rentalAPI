using bnbAPI.model;

namespace bnbAPI.Source.Svc
{
    public interface IReviewService
    {
        public void ReviewStay(Review review,int userId);
    }
}
