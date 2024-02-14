using TrainingRestFullApi.src.DTOs.Review;
using static TrainingRestFullApi.src.Configuration.ServiceResponse;

namespace TrainingRestFullApi.src.Interfaces
{
    public interface IReview
    {
        Task<GeneralResponse> Create(Guid movieGuid, Guid userGuid, ReviewDTO review);
        Task<GeneralResponse> Update(Guid guid, Guid userGuid, ReviewDTO review);
        Task<GeneralResponse> Delete(Guid guid, Guid userGuid);
    }
}
