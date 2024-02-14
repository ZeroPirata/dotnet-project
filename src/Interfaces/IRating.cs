using TrainingRestFullApi.src.DTOs.Rating;
using static TrainingRestFullApi.src.Configuration.ServiceResponse;

namespace TrainingRestFullApi.src.Interfaces
{
    public interface IRating
    {
        Task<GeneralResponse> LikeMovie(Guid movieGuid, Guid userGuid, RatingDTO ratingDTO);
        Task<GeneralResponse> RatingMovie(Guid movieGuid, Guid userGuid, RatingDTO ratingDTO);
        Task<GeneralResponse> ScoreMovie(Guid movieGuid, Guid userGuid, RatingDTO ratingDTO);
    }
}
