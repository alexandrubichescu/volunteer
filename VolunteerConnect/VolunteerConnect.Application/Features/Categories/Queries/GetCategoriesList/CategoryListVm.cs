namespace VolunteerConnect.Application.Features.Categories.Queries.GetCategoriesList;

public class CategoryListVm
{
    public Guid CategoryId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; } 
    public string? imageUrl { get; set; }
}
