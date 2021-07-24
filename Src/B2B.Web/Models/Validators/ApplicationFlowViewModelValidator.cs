using System.Collections.Generic;
using B2B.Shared.Dto.ApplicationFlow;
using B2B.Shared.Dto.Validators;
using FluentValidation;
using Newtonsoft.Json;

namespace B2B.Web.Models.Validators
{
    public class ApplicationFlowViewModelValidator : ValidatorBase<ApplicationViewModel>
    {
        private Dictionary<string, string> _formDict;

        public ApplicationFlowViewModelValidator()
        {
            RuleForEach(x => x.FlowDescription.Questions)
                .Must((model, question) => IsPresentInForm(model.FormJson, question))
                .WithMessage("This is a required question.");
        }

        private bool IsPresentInForm(string formJson, ApplicationFlowQuestion question)
        {
            _formDict ??= JsonConvert.DeserializeObject<Dictionary<string, string>>(formJson);
            return question.IsOptional || _formDict.ContainsKey(question.Code);
        }
    }
}
