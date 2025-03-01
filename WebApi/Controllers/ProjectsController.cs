using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController(IProjectService projectService) : ControllerBase
{
    private readonly IProjectService _projectService = projectService;

    [HttpPost]
    public async Task<IActionResult> Create(ProjectRegistrationForm registrationForm)
    {
        //Kollar om valid och om inte eller mindre än 1 så skickar tillbaka badrequest (400).
        if (!ModelState.IsValid && registrationForm.CustomerId < 1)
            return BadRequest();
        var result = await _projectService.CreateProjectAsync(registrationForm);
        return result.Success ? Created("", null) : Problem();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var projects = await _projectService.GetProjectsAsync();
        return Ok(projects);
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectUpdateForm updateForm)
    {
        var result = await _projectService.UpdateProjectAsync(updateForm);
        return result.Success ? Ok(result) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _projectService.RemoveProjectAsync(id);
        return result.Success ? Ok(result) : NotFound();
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProject(int id)
    {
        var project = await _projectService.GetProjectAsync(id);
        return Ok(project);
    }
}
