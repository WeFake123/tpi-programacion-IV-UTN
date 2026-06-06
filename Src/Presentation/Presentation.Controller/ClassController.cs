using Application.Dtos.Request;
using Application.Dtos.Responses;
using Application.Interfaces;
using Application.Services;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Presentation.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _service;
        private readonly IEmailService _emailService;

        public ClassController(IClassService service, IEmailService emailService)
        {
            _service = service;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> Get()
        {
            var classes = await _service.GetAll();

            return Ok(classes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> GetById(Guid id)
        {
            var gymClass = await _service.GetById(id);

            if (gymClass == null)
                return NotFound();

            return Ok(gymClass);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateClassRequest dto)
        {
            var gymClass = new Class
            {
                Name = dto.Name,
                Max_Users = dto.Max_Users,

            };

            await _service.Create(gymClass);

            var response = new ClassResponse
            {
                Id = gymClass.Id,
                Name = gymClass.Name,
                Max_Users = gymClass.Max_Users,

                Schedules = gymClass.Schedules.Select(s => new ScheduleResponse
                {
                    DayOfWeek = (int)s.DayOfWeek,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    IsActive = s.IsActive
                }).ToList()
            };

            return Ok(response);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] UpdateClassRequest dto)
        {
            if (dto == null)
                return BadRequest();

            var gymClass = new Class
            {
                Name = dto.Name!,
                Max_Users = dto.Max_Users
            };

            var updated = await _service.Update(id, gymClass);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.Delete(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        } 

        [HttpGet("test-email")]
        public async Task<IActionResult> TestEmail()
        {
            await _emailService.SendEmailAsync(
                "maximohahn0@gmail.com",
                "Prueba",
                "<h1>Hola desde Gym API</h1>");

            return Ok("Mail enviado");
        }
    }
}