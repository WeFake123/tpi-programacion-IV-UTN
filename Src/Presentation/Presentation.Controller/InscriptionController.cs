using Application.Dtos.Request;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Presentation.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class InscriptionController : ControllerBase
    {
        private readonly IInscriptionService _service;

        public InscriptionController(IInscriptionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Inscribe([FromBody] InscriptionRequest request)
        {
            var result = await _service.Inscribe(request);

            if (!result.Success)
                return BadRequest(result.ErrorMessage);

            return Ok(new { message = "Inscripción exitosa.", data = result.Data });
        }
    }
}