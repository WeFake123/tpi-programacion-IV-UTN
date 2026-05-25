using Application.Interfaces;
using Domain.Entity;
using Presentation.Controller;

namespace Presentation.Presentation.Controller
{


    public class AdminController : UsersController<Admin>
    {
        public AdminController(IUserService service, IAuthService authService) : base(service, authService)

        {
        }
    }
}
