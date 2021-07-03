using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace B2B.Web.Controllers
{
    public class ControllerBase : Controller
    {
        public ControllerBase(IMediator mediator)
        {
            Mediator = mediator;
        }

        protected IMediator Mediator { get; }

        protected void SetTitle(string title)
        {
            ViewBag.Title = title;
        }
    }
}