namespace TrainingRestFullApi.src.DTOs
{
    public record class UserSession(Guid? Id, string? Name, string? NickName, string? Email);
}
