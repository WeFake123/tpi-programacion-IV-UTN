using Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Dtos.Request;
using Application.Dtos.Responses;
using Application.Interfaces;
using Presentation.Authorization;

namespace Presentation.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _service;

        public ScheduleController(IScheduleService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> Get()
        {
            var schedules = await _service.GetAll();

            return Ok(schedules);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetById(Guid id)
        {
            var schedule = await _service.GetById(id);

            if (schedule == null)
                return NotFound();

            return Ok(schedule);
        }

        [Authorize(Policy = Policies.AdminOSysAdmin)]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateScheduleRequest dto)
        {
            var schedule = new Schedule
            {
                DayOfWeek = (Day)dto.DayOfWeek,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                IsActive = true
            };

            if (schedule.EndTime <= schedule.StartTime)
                return BadRequest("EndTime must be greater than StartTime.");

            var created = await _service.Create(schedule);

            var response = new ScheduleResponse
            {

                DayOfWeek = (int)created.DayOfWeek,
                StartTime = created.StartTime,
                EndTime = created.EndTime,
                IsActive = created.IsActive
            };

            return Ok(response);
        }

        [Authorize(Policy = Policies.AdminOSysAdmin)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] UpdateScheduleRequest dto)
        {
            var schedule = new Schedule
            {
                DayOfWeek = (Day)dto.DayOfWeek,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                IsActive = dto.IsActive
            };

            if (schedule.EndTime <= schedule.StartTime)
                return BadRequest("EndTime must be greater than StartTime.");

            var updated = await _service.Update(id, schedule);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [Authorize(Policy = Policies.AdminOSysAdmin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var schedule = await _service.GetById(id);

            if (schedule == null)
                return NotFound();

            var deleted = await _service.Delete(schedule);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}