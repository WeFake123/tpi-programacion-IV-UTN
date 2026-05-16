using Application.Interfaces;
using Domain.Entity;
using Presentation.Controller;

namespace Presentation.Presentation.Controller
{


    public class AdminController : UsersController<Admin>
    {
        public AdminController(IUserService service, IAuthService authService) : base(service, authService)

        {
            // Aquí puedes agregar métodos que SOLO existan para admins
        }
    }
}
