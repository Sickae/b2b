using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace B2B.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        public HomeController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
