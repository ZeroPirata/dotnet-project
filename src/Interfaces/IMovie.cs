using Microsoft.AspNetCore.Mvc;
using TrainingRestFullApi.src.DTOs.Movie;
using static TrainingRestFullApi.src.Configuration.ServiceResponse;

namespace TrainingRestFullApi.src.Interfaces
{
    public interface IMovie
    {
        Task<GeneralResponse> Create(MCreateDTO creaetDto);
        Task<GeneralResponse> Update(Guid id, MUpdateDTO updateDTO);
        Task<GeneralResponse> UpdateCrew(Guid id, MUpdateCrewDTO crewDTO);
        Task<DynamicResponse> ReadOne(Guid id);
        Task<DynamicResponse> ReadMany();
        Task<GeneralResponse> Delete(Guid id);
        Task<DynamicResponse> GetByCrew(string craw, string? crawName);
    }
}
