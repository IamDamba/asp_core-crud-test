using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models.Entities;

namespace server.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class TodosController : ControllerBase
{
    readonly private AppDbContext _dbContext;
    readonly private IConfiguration _configuration;

    public TodosController(AppDbContext dbContext, IConfiguration configuration)
    {
        this._dbContext = dbContext;
        this._configuration = configuration;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var todo_list = await this._dbContext.Todos.ToListAsync();
        return Ok(todo_list);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> One(int id)
    {
        var todo = await this._dbContext.Todos.FindAsync(id);
        return todo is null ? NotFound() : Ok(todo);
    }
}