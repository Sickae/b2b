using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using B2B.Logic.BusinessLogic.Application.Query;
using B2B.Logic.BusinessLogic.Blacklist.Query;
using B2B.Shared.Dto;
using B2B.Shared.Dto.ApplicationFlow;
using B2B.Shared.Dto.Validators;
using B2B.Shared.Enums;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;

namespace B2B.Web.Models.Validators
{
    public class ApplicationViewModelValidator : ValidatorBase<ApplicationViewModel>
    {
        private readonly IMediator _mediator;
        private Dictionary<string, string> _formDict;
        private bool _isExistingApplicationChecked;
        private ApplicationDto _existingApplication;

        public ApplicationViewModelValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleForEach(x => x.FlowDescription.Questions)
                .Must((model, question) => IsPresentInForm(model.FormJson, question))
                .WithMessage("This is a required question.");

            RuleFor(x => x.InGameName)
                .MustAsync((x, token) => HasNoApplicationWithStatus(x, ApplicationStatus.Pending, token))
                .WithMessage("There is a pending application for this user already.")
                .MustAsync((x, token) => HasNoApplicationWithStatus(x, ApplicationStatus.Approved, token))
                .WithMessage("This user is a member already.")
                .MustAsync((x, token) => HasNoApplicationWithStatus(x, ApplicationStatus.Rejected, token))
                .WithMessage("This user's application has been recently rejected. Try again later.")
                .MustAsync(IsNotBlacklisted)
                .WithMessage("This user is blacklisted.");
        }

        private bool IsPresentInForm(string formJson, ApplicationFlowQuestion question)
        {
            _formDict ??= JsonConvert.DeserializeObject<Dictionary<string, string>>(formJson);
            return question.IsOptional || _formDict.ContainsKey(question.Code);
        }

        private async Task<bool> HasNoApplicationWithStatus(string inGameName, ApplicationStatus status, CancellationToken cancellationToken)
        {
            await LoadRelatedApplicationOrDoNothing(inGameName, cancellationToken);
            if (_existingApplication == null)
                return true;

            if (status is ApplicationStatus.Rejected)
                // TODO timeSpan from config
                return _existingApplication.Status != status ||
                       _existingApplication.StatusCompleteDateUtc.Add(TimeSpan.FromDays(14)) < DateTime.UtcNow;

            return _existingApplication.Status != status;
        }

        private async Task LoadRelatedApplicationOrDoNothing(string inGameName, CancellationToken cancellationToken)
        {
            if (!_isExistingApplicationChecked)
            {
                _existingApplication =
                    await _mediator.Send(new ApplicationQuery {InGameName = inGameName}, cancellationToken);
                _isExistingApplicationChecked = true;
            }
        }

        private async Task<bool> IsNotBlacklisted(string inGameName, CancellationToken cancellationToken)
        {
            var blacklistEntity =
                await _mediator.Send(new BlacklistQuery {InGameName = inGameName}, cancellationToken);
            return blacklistEntity == null;
        }
    }
}
