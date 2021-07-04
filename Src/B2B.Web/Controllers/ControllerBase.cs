using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace B2B.Web.Controllers
{
    public class ControllerBase : Controller
    {
        public ControllerBase(IMediator mediator, IMapper mapper)
        {
            Mediator = mediator;
            Mapper = mapper;
        }

        protected IMediator Mediator { get; }
        protected IMapper Mapper { get; }

        protected void SetTitle(string title)
        {
            ViewBag.Title = title;
        }
    }
}
