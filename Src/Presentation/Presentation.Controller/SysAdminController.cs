using Application.Interfaces;
using Domain.Entity;
using Infraestructure.Service;
using Presentation.Controller;

namespace Presentation.Presentation.Controller
{


    public class SysAdminController : UsersController<SysAdmin>
    {
        public SysAdminController(IUserService service, IAuthService authService) : base(service, authService)
        {
            // Aquí puedes agregar métodos que SOLO existan para Clientes
        }
    }
}

