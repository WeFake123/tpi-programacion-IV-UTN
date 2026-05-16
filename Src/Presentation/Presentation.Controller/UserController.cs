using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Interfaces;
using Domain.Entity;
using Infraestructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controller
{
    [ApiController]
    // Usamos esta ruta para que los hijos hereden la ruta base o la definan ellos
    [Route("api/[controller]")]
    public abstract class UsersController<T> : ControllerBase where T : User
    {
        protected readonly IUserService _service;
        protected readonly IAuthService _authService;

        public UsersController(IUserService service, IAuthService authService)
        {
            _service = service;
            _authService = authService;
        }
        [AllowAnonymous]
        [HttpPost("singin")]
        public async Task<ActionResult<SingInResponse>> SingIn([FromBody] SingInRequest request)
        {
            var response = await _authService.SingIn(request);

            if (response == null)
                return Unauthorized("Credenciales incorrectas.");

            return Ok(response);
        }



        [AllowAnonymous]
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<T>>> Get()
        {
            var users = await _service.GetAll();
            // Filtramos para devolver solo el tipo específico (Client, Admin, etc.)
            return Ok(users.OfType<T>());
        }
        [Authorize]
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<T>> GetById(Guid id)
        {
            var user = await _service.GetById(id);
            if (user is not T typedUser) return NotFound();

            return Ok(typedUser);
        }
        [AllowAnonymous]
        [HttpPost]
        public virtual async Task<ActionResult<T>> Post(T user)
        {
            if (user == null) return BadRequest();

            // La validación básica se mantiene
            if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email))
                return BadRequest("Name and Email are required.");

            var created = await _service.Create(user);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, (T)created);
        }
        [Authorize]
        [HttpPatch("{id}")]
        public virtual async Task<IActionResult> Patch(Guid id, T user)
        {
            var updated = await _service.Update(id, user);
            if (!updated) return NotFound();

            return NoContent();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.Delete(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
