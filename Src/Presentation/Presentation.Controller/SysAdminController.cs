<<<<<<< HEAD
﻿using Application.Dtos.Request;
using Application.Interfaces;
using Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controller;


namespace Presentation.Presentation.Controller
{


    public class SysAdminController : UsersController<SysAdmin>
    {
        private readonly ISysAdminService _sysAdminService;
        public SysAdminController(IUserService service, IAuthService authService, ISysAdminService sysAdminService) : base(service, authService)
        {
            _sysAdminService = sysAdminService;

        }

        [Authorize]
        [HttpPost("UpgradeUsersRol")]
        public async Task<ActionResult> UpgradeUsersRol([FromBody] UpgradeUsersRol request)
        {
            var result = await _sysAdminService.UpgradeUsersRol(request);

            return Ok(new
            {
                Message = "Role updated successfully",
                User = result.Email
            });
        }
    }
}

=======
﻿using Application.Dtos.Request;
using Application.Interfaces;
using Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Authorization;
using Presentation.Controller;


namespace Presentation.Presentation.Controller
{

    [Authorize(Policy = Policies.SoloSysAdmin)]
    public class SysAdminController : UsersController<SysAdmin>
    {
        private readonly ISysAdminService _sysAdminService;
        public SysAdminController(IUserService service, IAuthService authService, ISysAdminService sysAdminService) : base(service, authService)
        {
            _sysAdminService = sysAdminService;

        }

        
        [HttpPost("UpgradeUsersRol")]
        public async Task<ActionResult> UpgradeUsersRol([FromBody] UpgradeUsersRol request)
        {

            var result = await _sysAdminService.UpgradeUsersRol(request);

            if (result == null)
                return NotFound();

            return Ok(new
            {
                Message = "Rol actualizado correctamente",
                User = result.Email
            });
        }
    }
}

>>>>>>> 6468ebf9de9ccde0469555710311bd79a61f3766
