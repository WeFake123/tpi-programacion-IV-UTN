using Application.Interfaces;
using Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Authorization;
using Presentation.Controller;

namespace Presentation.Presentation.Controller
{
    [Authorize(Policy = Policies.AdminOSysAdmin)]
    public class ClientController : UsersController<Client>
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService service, IAuthService authService)
            : base(service, authService)
        {
            _clientService = service;
        }

        [HttpPatch("{id}")]
        public override async Task<IActionResult> Patch(Guid id, Client user)
        {
            await _clientService.Update(id, user);
            return NoContent();
        }
    }
}
