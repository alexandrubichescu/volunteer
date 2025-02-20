using MediatR;

namespace VolunteerConnect.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommand: IRequest<CreateCategoryCommandResponse>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;

}
