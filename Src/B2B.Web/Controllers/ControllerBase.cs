using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace B2B.Web.Controllers
{
    public class ControllerBase : Controller
    {
        protected IMediator Mediator { get; private set; }

        public ControllerBase(IMediator mediator)
        {
            Mediator = mediator;
        }

        protected void SetTitle(string title)
        {
            ViewBag.Title = title;
        }
    }
}
