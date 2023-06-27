using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models.Dtos;
using server.Models.Entities;

namespace server.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class MembersController : ControllerBase
{
    readonly private AppDbContext _dbContext;
    readonly private IConfiguration _configuration;

    public MembersController(AppDbContext dbContext, IConfiguration configuration)
    {
        this._dbContext = dbContext;
        this._configuration = configuration;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var member_list = await this._dbContext.Members.ToListAsync();
        return Ok(member_list);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> One(int id)
    {
        var member = await this._dbContext.Members.FindAsync(id);
        return member is null ? NotFound() : Ok(member);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(MembersEntity _member)
    {
        var member_list = await this._dbContext.Members.ToListAsync();
        var member =
            member_list.Find(mem => mem.Email == _member.Email && MembersEntity.VerifyPwd(_member.Pwd, mem.Pwd));

        if (member is null)
            BadRequest();

        var result = new MembersDto()
        {
            Id = member.Id,
            Email = member.Email,
        };

        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(MembersEntity _member)
    {
        var member_list = await this._dbContext.Members.ToListAsync();
        var member =
            member_list.Find(mem => mem.Email == _member.Email && MembersEntity.VerifyPwd(_member.Pwd, mem.Pwd));

        if (member is not null)
            BadRequest();

        var new_mem = this._dbContext.Members.Add(new MembersEntity()
        {
            Email = _member.Email,
            Pwd = MembersEntity.HashPwd(_member.Pwd),
        });

        var result = new MembersDto()
        {
            Id = new_mem.Entity.Id,
            Email = new_mem.Entity.Email,
        };

        await this._dbContext.SaveChangesAsync();

        return Created($"/api/v1/members/{new_mem.Entity.Id}", result);
    }
}