using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using TrainingRestFullApi.src.DTOs.Movie;
using TrainingRestFullApi.src.Entities;
using TrainingRestFullApi.src.Interfaces;
using static TrainingRestFullApi.src.Configuration.ServiceResponse;

namespace TrainingRestFullApi.src.Service
{
    public class MovieService(ApplicationDbContext context, IMapper mapper) : IMovie
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        public async Task<GeneralResponse> Create(MCreateDTO createDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (createDto == null) return new GeneralResponse(406, "Model is empty");
                Movie movie = new()
                {
                    Cast = createDto.Cast,
                    Description = createDto.Description!,
                    Genere = createDto.Genere!,
                    Title = createDto.Title!,
                };
                if (createDto.Crew != null)
                {
                    movie.AddCrewMember(createDto.Crew!);
                }
                await _context.Movies.AddAsync(movie);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new GeneralResponse(200, "Movie created successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao cadastrar novo filme dentro do service: {ex.Message}");
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<DynamicResponse> ReadMany()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Movie classMovie = new();
                List<Movie> getAllMovies = await classMovie.GetAllMovies(_context);
                await transaction.CommitAsync();
                return new DynamicResponse(200, "All Movies", getAllMovies); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error to get all movies: {ex.Message}");
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<DynamicResponse> GetByCrew(string crew, string? crewName)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Movie classMovie = new();
                if (crew == null || crewName == null) return new DynamicResponse(406, "Paramets are necessery", new { });
                string[] crewList = crew.Replace(" ", "").Split(',');
                List<Movie> results = [];
                var query = await classMovie.GetAllMovies(_context);
                foreach (var item in crewList)
                {
                    var find = query.Where(c =>
                    {
                        var crewDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(c.Crew!);
                        if (!crewDictionary!.ContainsKey(item))
                            return false;

                        return crewDictionary[item] == crewName;
                    });
                    results.AddRange(find);
                }
                if (results.Count == 0) return new DynamicResponse(404, "Not Found crew or crew name", new { });
                await transaction.CommitAsync();
                return new DynamicResponse(200, "Success", results);
            }
            catch (Exception ex)
            {
                Console.Write($"Error to get crew or crew name: {ex.Message}");
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<GeneralResponse> Update(Guid guid, MUpdateDTO updateDtop)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == guid);
                if (movie == null)
                {
                    return new GeneralResponse(404, "Movie not found");
                }
                _mapper.Map(updateDtop, movie);
                _context.Movies.Update(movie);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new GeneralResponse(200, "Update successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error to update movie: {ex.Message}");
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<GeneralResponse> UpdateCrew(Guid guid, MUpdateCrewDTO crewDTO)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (crewDTO == null || guid == Guid.Empty) return new GeneralResponse(406, "Model is empty");
                var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == guid);
                if (movie == null) return new GeneralResponse(404, "Movie not found");
                var movieCrew = JsonConvert.DeserializeObject<Dictionary<string, string>>(movie.Crew);

                if (crewDTO.Crew != null && crewDTO.Crew != movieCrew)
                {
                    movie.AddCrewMember(crewDTO.Crew!);
                }
                _context.Update(movie);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new GeneralResponse(200, "Movie crew updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error to update movie crew: ${ex.Message}");
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<DynamicResponse> ReadOne(Guid id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            Movie classMovie = new();
            try
            {
                if (id == Guid.Empty) return new DynamicResponse(400, "Invalid ID", new { });

                Movie? getMovieByGuid = await classMovie.GetMovieByGuid(_context, id);
                if (getMovieByGuid == null) return new DynamicResponse(404, "Movie Not Found", new { });
                await transaction.CommitAsync();
                return new DynamicResponse(200, "Success", getMovieByGuid);
            }
            catch (Exception ex)
            {
                Console.Write($"Error to get movie by Guid: {ex.Message}");
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<GeneralResponse> Delete(Guid id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (id == Guid.Empty) return new GeneralResponse(400, "Invalid ID");
                Movie? findMovie = await _context.Movies.FindAsync(id);
                if (findMovie == null) return new GeneralResponse(404, "Movie Not Found");
                _context.Movies.Remove(findMovie);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new GeneralResponse(200, "Movie Deleted successfully");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.Write($"Error to delete movie: {ex.Message}");
                throw;
            }
        }
    }
}
