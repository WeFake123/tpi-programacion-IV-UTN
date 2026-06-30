
using Application.Interfaces;
using Application.Dtos.Request;
using Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Authorization;

namespace Presentation.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class UsersController<T> : ControllerBase where T : User
    {
        protected readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [Authorize(Policy = Policies.AdminOSysAdmin)]
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<T>>> Get()
        {
            var users = await _service.GetAll();
            return Ok(users.OfType<T>());
        }

        [Authorize]
        [HttpPut("me")]
        public async Task<IActionResult> UpdateProfile(UpdateUserRequest request)
        {
            var result = await _service.UpdateUser(request);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("claims")]
        public IActionResult Claims()
        {
            return Ok(User.Claims.Select(c => new
            {
                c.Type,
                c.Value
            }));
        }

        [Authorize(Policy = Policies.AdminOSysAdmin)]
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<T>> GetById(Guid id)
        {
            var user = await _service.GetById(id);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost]
        public virtual async Task<ActionResult<T>> Post(T user)
        {
            var created = await _service.Create(user);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, (T)created);
        }

        [Authorize(Policy = Policies.AdminOSysAdmin)]
        [HttpPatch("{id}")]
        public virtual async Task<IActionResult> Patch(Guid id, T user)
        {
            await _service.Update(id, user);
            return NoContent();
        }

        [Authorize(Policy = Policies.AdminOSysAdmin)]
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}