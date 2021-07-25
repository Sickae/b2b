using System.Threading.Tasks;
using AutoMapper;
using B2B.Logic.BusinessLogic.Application.Command;
using B2B.Logic.BusinessLogic.ApplicationFlow.Query;
using B2B.Logic.Identity;
using B2B.Shared.Dto;
using B2B.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace B2B.Web.Controllers
{
    public class ApplicationController : ControllerBase
    {
        private readonly IdentityUserManager _identityUserManager;

        public ApplicationController(IMediator mediator, IMapper mapper,
            IdentityUserManager identityUserManager) : base(mediator, mapper)
        {
            _identityUserManager = identityUserManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Fill));
        }

        [HttpGet]
        public async Task<IActionResult> Fill()
        {
            var applicationFlow = await Mediator.Send(new StaticApplicationFlowQuery());
            var model = Mapper.Map<ApplicationViewModel>(applicationFlow);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Fill(ApplicationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var applicationFlow = await Mediator.Send(new StaticApplicationFlowQuery());
                Mapper.Map(applicationFlow, model);
                return View(model);
            }

            var application = Mapper.Map<ApplicationDto>(model);
            var result = await Mediator.Send(new CreateApplicationCommand {Dto = application});
            return Json(result);
        }
    }
}
