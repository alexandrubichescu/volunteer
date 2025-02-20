namespace VolunteerConnect.Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string title, object key)
        : base($"{title} ({key}) is not found")
    {
    }
}
