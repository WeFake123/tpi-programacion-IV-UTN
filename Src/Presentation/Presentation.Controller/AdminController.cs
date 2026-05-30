using Application.Dtos.Request.Admin;
using Application.Interfaces;
using Application.Services;
using Azure.Core;
using Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controller;

namespace Presentation.Presentation.Controller
{


    public class AdminController : UsersController<Admin>
    {

        private readonly IAdminService _AdminService;
        public AdminController(IUserService service, IAuthService authService, IAdminService adminService) : base(service, authService)
        {
            _AdminService = adminService;
        }          
        
        //Borrar horarios de clases

        //Modificar horarios de clases

        //Crear una clase con horaris deseados

        [Authorize]
        [HttpPost("CreteClass")]
        public async Task<ActionResult> CreteClass([FromBody] CreateClassWithSchedulesRequest request)

        {

            var result = await _AdminService.CreteClass(request.ClassRequest, request.ScheduleRequests);
            if(result == null)
            {
                return BadRequest("Datos incorrectos");
            }

            return Ok(new
            {
                Message = "Clase creada correctamente",
                Class = result?.Name
            });

        }

    }
}
