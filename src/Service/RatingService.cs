using TrainingRestFullApi.src.DTOs.Rating;
using TrainingRestFullApi.src.Entities;
using TrainingRestFullApi.src.Interfaces;
using static TrainingRestFullApi.src.Configuration.ServiceResponse;

namespace TrainingRestFullApi.src.Service
{
    public class RatingService(ApplicationDbContext context) : IRating
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<GeneralResponse> LikeMovie(Guid movieGuid, Guid userGuid, RatingDTO ratingDTO)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var movieFind = _context.Movies.SingleOrDefault(m => m.Id == movieGuid);
                if (movieFind == null) { return new GeneralResponse(404, "Movie not found"); }
                var userLiked = _context.UserLikedMovies.SingleOrDefault(lkd => lkd.UserId == userGuid);
                if(userLiked != null)
                {
                    userLiked!.LikedMovie = ratingDTO.Like;
                }
                else
                {
                    UserLikedMovie lkdUser = new()
                    {
                        UserId = userGuid,
                        MovieId = movieGuid,
                        LikedMovie = ratingDTO.Like
                    };
                    _context.UserLikedMovies.Add(lkdUser);
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new GeneralResponse(200, "Ok");
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<GeneralResponse> RatingMovie(Guid movieGuid, Guid userGuid, RatingDTO ratingDTO)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var movieFind = _context.Movies.SingleOrDefault(m => m.Id == movieGuid);
                if (movieFind == null) { return new GeneralResponse(404, "Movie not found"); }
                var userLiked = _context.UserLikedMovies.SingleOrDefault(lkd => lkd.UserId == userGuid);
                if (userLiked != null)
                {
                    userLiked.AssessmentedMovie = ratingDTO.Assessmeted;
                }
                else
                {
                    UserLikedMovie lkdUser = new()
                    {
                        UserId = userGuid,
                        MovieId = movieGuid,
                        AssessmentedMovie = ratingDTO.Assessmeted
                    };
                    _context.UserLikedMovies.Add(lkdUser);
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new GeneralResponse(200, "Ok");
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<GeneralResponse> ScoreMovie(Guid movieGuid, Guid userGuid, RatingDTO ratingDTO)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var movieFind = _context.Movies.SingleOrDefault(m => m.Id == movieGuid);
                if (movieFind == null) { return new GeneralResponse(404, "Movie not found"); }
                var userLiked = _context.UserLikedMovies.SingleOrDefault(lkd => lkd.UserId == userGuid);
                decimal score = MapearNota(ratingDTO.Score);
                decimal oldScore = MapearNota(userLiked!.Score);
                if (userLiked != null)
                {
                    userLiked!.Score = score;
                }
                else
                {
                    UserLikedMovie lkdUser = new()
                    {
                        UserId = userGuid,
                        MovieId = movieGuid,
                        Score = score
                    };
                    await _context.UserLikedMovies.AddAsync(lkdUser);
                }
                movieFind.UpdateScore(score.ToString(), oldScore.ToString(), movieFind.Ratings!);
                _context.UpdateRange(movieFind);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new GeneralResponse(200, "Ok");
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public int MapearNota(decimal nota)
        {
            if (nota >= 0 && nota <= 1)
            {
                return 1;
            }
            else if (nota >= 1.1m && nota <= 2)
            {
                return 2;
            }
            else if (nota >= 2.1m && nota <= 3)
            {
                return 3;
            }
            else if (nota >= 3.1m && nota <= 4)
            {
                return 4;
            }
            else if (nota >= 4.1m && nota <= 5)
            {
                return 5;
            }
            else
            {
                return 0;
            }
        }
    }
}
