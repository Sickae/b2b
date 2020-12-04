using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace B2B.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        public HomeController(IMediator mediator) : base(mediator)
        { }

        public IActionResult Index()
        {
            return View();
        }
    }
}