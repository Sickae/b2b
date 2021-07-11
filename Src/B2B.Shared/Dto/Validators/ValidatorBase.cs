using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using B2B.Shared.Attributes;
using B2B.Shared.Helpers;
using FluentValidation;

namespace B2B.Shared.Dto.Validators
{
    public class ValidatorBase<TModel> : AbstractValidator<TModel>
    {
        public ValidatorBase()
        {
            ApplyBaseRules();
        }

        public void ApplyRuleFor<TProperty>(Expression<Func<TModel, TProperty>> expression,
            AppValidationAttribute validationAttribute)
        {
            var errorMsg = validationAttribute.ErrorMessage;
            switch (validationAttribute)
            {
                case RequiredAttribute requiredAttribute:
                {
                    var requiredRule = RuleFor(expression).NotEmpty();
                    SetRuleErrorMessage(requiredRule, errorMsg);
                    break;
                }
                case StringLengthAttribute stringLengthAttribute when expression.ReturnType == typeof(string):
                    var stringLengthMinLengthRule = RuleFor(expression as Expression<Func<TModel, string>>)
                        .MinimumLength(stringLengthAttribute.MinimumLength);
                    SetRuleErrorMessage(stringLengthMinLengthRule, errorMsg);

                    var stringLengthMaxLengthRule = RuleFor(expression as Expression<Func<TModel, string>>)
                        .MaximumLength(stringLengthAttribute.MaximumLength);
                    SetRuleErrorMessage(stringLengthMaxLengthRule, errorMsg);
                    break;
            }
        }

        private void SetRuleErrorMessage<T, TProperty>(IRuleBuilderOptions<T, TProperty> rule, string errorMessage)
        {
            if (!string.IsNullOrEmpty(errorMessage))
                rule.WithMessage(errorMessage);
        }

        private void ApplyRuleForProperty(PropertyInfo property, AppValidationAttribute validationAttribute)
        {
            var methodInfo = GetType().GetMethod(nameof(ApplyRuleFor));
            var argumentTypes = new[] {property.PropertyType};
            var genericMethod = methodInfo.MakeGenericMethod(argumentTypes);
            var propertyExpression = ExpressionHelper.CreateMemberExpressionForProperty<TModel>(property);
            genericMethod.Invoke(this, new[] {propertyExpression, validationAttribute});
        }

        private PropertyInfo[] GetProperties()
        {
            return typeof(TModel).GetProperties().Where(x => x.IsDefined(typeof(AppValidationAttribute))).ToArray();
        }

        private void ApplyBaseRules()
        {
            var properties = GetProperties();
            if (properties is not {Length: > 0})
                return;

            foreach (var prop in properties)
            {
                var validationAttributes = prop.GetCustomAttributes<AppValidationAttribute>();
                foreach (var validationAttr in validationAttributes)
                    ApplyRuleForProperty(prop, validationAttr);
            }
        }
    }
}
