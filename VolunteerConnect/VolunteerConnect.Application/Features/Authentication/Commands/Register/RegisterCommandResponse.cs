namespace VolunteerConnect.Application.Features.Authentication.Commands.Register;

public class RegisterCommandResponse
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = new();
}
