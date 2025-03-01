using Data.Contexts;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.Repositories;

public class ProjectRepository_Tests
{
    private readonly DataContext _context;
    private readonly IProjectRepository _projectRepository;

    public ProjectRepository_Tests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid}");


        _context = new DataContext();
        _projectRepository = new ProjectRepository(_context);
    }

}
