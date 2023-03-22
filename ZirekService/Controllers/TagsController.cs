using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ZirekService.Data;
using ZirekService.Models.Entities;
using ZirekService.Services;

namespace ZirekService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase {
        private readonly ApplicationDbContext _context;
        public TagsController(ApplicationDbContext context) {
            _context = context;
        }
        [HttpPost("/Create")]
        [Authorize(Roles = RoleService.AdminRole)]
        public IActionResult Create(string tagValue) {
            var tag = _context.Tags.Where(x => x.Value == tagValue).FirstOrDefault();
            if (tag != null)
                return BadRequest("this type already exists");
            _context.Tags.Add(new TagEntity() { Value = tagValue });
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet("/DeleteType")]
        [Authorize(Roles = RoleService.AdminRole)]
        public IActionResult DeleteType(int id) {
            var tag = _context.Tags.Where(x => x.Id == id).FirstOrDefault();
            if (tag == null)
                return NotFound();
            _context.Tags.Remove(tag);
            _context.SaveChanges();
            return Ok();
        }
    }
}
