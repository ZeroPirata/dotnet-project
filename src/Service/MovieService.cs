using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using TrainingRestFullApi.src.DTOs;
using TrainingRestFullApi.src.DTOs.Movie;
using TrainingRestFullApi.src.Entities;
using TrainingRestFullApi.src.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static TrainingRestFullApi.src.Configuration.ServiceResponse;

namespace TrainingRestFullApi.src.Service
{
    public class MovieService(ApplicationDbContext context) : IMovie
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<GeneralResponse> Create(MCreateDTO createDto)
        {
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
                return new GeneralResponse(200, "Movie created successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao cadastrar novo filme dentro do service: {ex.Message}");
                throw;
            }
        }

        public async Task<DynamicResponse> ReadMany()
        {
            try
            {
                Movie classMovie = new();
                List<Movie> getAllMovies = await classMovie.GetAllMovies(_context);
                return new DynamicResponse(200, "All Movies", getAllMovies); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error to get all movies: {ex.Message}");
                throw;
            }
        }

        public async Task<DynamicResponse> GetByCrew(string crew, string? crewName)
        {
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
                return new DynamicResponse(200, "Success", results);
            }
            catch (Exception ex)
            {
                Console.Write($"Error to get crew or crew name: {ex.Message}");
                throw;
            }
        }

        public Task<CreatedAtActionResult> Update(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<DynamicResponse> ReadOne(Guid id)
        {
            Movie classMovie = new();
            try
            {
                if (id == Guid.Empty) return new DynamicResponse(400, "Invalid ID", new { });

                Movie? getMovieByGuid = await classMovie.GetMovieByGuid(_context, id);
                if (getMovieByGuid == null) return new DynamicResponse(404, "Movie Not Found", new { });

                return new DynamicResponse(200, "Success", getMovieByGuid);
            }
            catch (Exception ex)
            {
                Console.Write($"Error to get movie by Guid: {ex.Message}");
                throw;
            }
        }

        public async Task<GeneralResponse> Delete(Guid id)
        {
            try
            {
                if (id == Guid.Empty) return new GeneralResponse(400, "Invalid ID");

                Movie? findMovie = await _context.Movies.FindAsync(id);
                if (findMovie == null) return new GeneralResponse(404, "Movie Not Found");

                _context.Movies.Remove(findMovie);
                await _context.SaveChangesAsync();

                return new GeneralResponse(200, "Movie Deleted successfully");
            }
            catch (Exception ex)
            {
                Console.Write($"Error to delete movie: {ex.Message}");
                throw;
            }
        }
    }
}
