using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using TrainingRestFullApi.src.DTOs.Review;
using TrainingRestFullApi.src.Entities;
using TrainingRestFullApi.src.Interfaces;
using static TrainingRestFullApi.src.Configuration.ServiceResponse;

namespace TrainingRestFullApi.src.Service
{
    public class ReviewService(ApplicationDbContext context) : IReview
    {

        private readonly ApplicationDbContext _context = context;

        public async Task<GeneralResponse> Create(Guid movieGuid, Guid userGuid, ReviewDTO reviewDTO)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (reviewDTO == null || movieGuid == Guid.Empty) return new GeneralResponse(406, "Paramets or Modal is empty");
                var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == movieGuid);
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userGuid);
                if (movie == null) return new GeneralResponse(404, "Movie not found");
                Review review = new()
                {
                    Comment = reviewDTO.Comment,
                    MovieId = movie.Id,
                    UserId = userGuid,
                    User = user
                };
                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new GeneralResponse(200, "New comment append to movie");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Error creting comment: {ex.Message}");
                throw;
            }
        }

        public async Task<GeneralResponse> Delete(Guid guid, Guid userGuid)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (guid == Guid.Empty || userGuid == Guid.Empty) return new GeneralResponse(406, "Paramets is required");
                var review = await _context.Reviews.SingleOrDefaultAsync(r => r.Id == guid);
                if (review == null) return new GeneralResponse(404, "Comment not found");
                if (review.UserId != userGuid) return new GeneralResponse(401, "Unathorized to delete the comment");
                _context.Reviews.Remove(review!);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new GeneralResponse(200, "Deleted successfull");
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<GeneralResponse> Update(Guid guid, Guid userGuid, ReviewDTO reviewDTO)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (userGuid == Guid.Empty || userGuid == Guid.Empty) return new GeneralResponse(406, "Paramets is required");
                var review = await _context.Reviews.SingleOrDefaultAsync(r => r.Id == guid);
                if (review == null) return new GeneralResponse(404, "Comment not found"); 
                if (review.UserId != userGuid) return new GeneralResponse(401, "Unathorized to update the comment");
                review.Comment = reviewDTO.Comment;
                review.UpdateAt = DateTime.Now.ToUniversalTime();
                _context.Update(review);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new GeneralResponse(200, "Comment updated successfull");
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }

        }
    }
}
