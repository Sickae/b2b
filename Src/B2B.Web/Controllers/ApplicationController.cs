using System.Linq;
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

        [HttpGet]
        public IActionResult List()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Applications(GridModel model)
        {
            // TOOD test data only, this needs abstraction
            var mockData = new[]
            {
                new {Name = "TestUser 1", Email = "test_1@email.com"},
                new {Name = "TestUser 2", Email = "test_2@email.com"},
                new {Name = "TestUser 3", Email = "test_3@email.com"},
                new {Name = "TestUser 4", Email = "test_4@email.com"},
                new {Name = "TestUser 5", Email = "test_5@email.com"},
                new {Name = "TestUser 6", Email = "test_6@email.com"},
                new {Name = "TestUser 7", Email = "test_7@email.com"},
                new {Name = "TestUser 8", Email = "test_8@email.com"},
                new {Name = "TestUser 9", Email = "test_9@email.com"},
                new {Name = "TestUser 10", Email = "test_10@email.com"},
                new {Name = "TestUser 11", Email = "test_11@email.com"},
                new {Name = "TestUser 12", Email = "test_12@email.com"},
            };
            var totalCount = mockData.Length;

            if (!string.IsNullOrEmpty(model.Search))
                mockData = mockData.Where(x => x.Name.Contains(model.Search)).ToArray();

            if (model.PageIndex.HasValue && model.PaginationLimit is > 0)
                mockData = mockData.Skip(model.PageIndex.Value * model.PaginationLimit.Value).Take(model.PaginationLimit.Value).ToArray();

            var gridData = new GridData
            {
                Result = mockData,
                TotalCount = totalCount
            };
            return Json(gridData);
        }
    }
}
