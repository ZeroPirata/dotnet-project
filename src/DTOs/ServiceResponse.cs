namespace TrainingRestFullApi.src.DTOs
{
    public class ServiceResponse
    {
        public record class GeneralResponse(int Flag, string Message);
        public record class LoginResponse(int Flag, string? Token, string Message);
    }
}
