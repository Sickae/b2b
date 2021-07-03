using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace B2B.Web.Infrastructure.ModelBinders
{
    /// <summary>
    ///     https://stackoverflow.com/a/46308876
    /// </summary>
    public class DateTimeModelBinder : IModelBinder
    {
        private static readonly string[] _dateTimeFormats =
            {"yyyyMMdd'T'HHmmss.FFFFFFFK", "yyyy-MM-dd'T'HH:mm:ss.FFFFFFFK"};

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var stringValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;

            if (bindingContext.ModelType == typeof(DateTime?) && string.IsNullOrEmpty(stringValue))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            bindingContext.Result = DateTime.TryParseExact(stringValue, _dateTimeFormats,
                CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var result)
                ? ModelBindingResult.Success(result)
                : ModelBindingResult.Failed();

            return Task.CompletedTask;
        }
    }
}