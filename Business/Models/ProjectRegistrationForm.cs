namespace Business.Models;

public class ProjectRegistrationForm
{
    public string ProjectName { get; set; } = null!;
    public string? Description { get; set; }
    public int CustomerId { get; set; }
}
