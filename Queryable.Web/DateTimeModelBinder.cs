using System;
using System.Globalization;
using System.Web.Mvc;

namespace Queryable.Web
{
    public class DateTimeModelBinder : DefaultModelBinder
    {
        private readonly string _customFormat;

        public DateTimeModelBinder(string customFormat)
        {
            _customFormat = customFormat;
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value == null || String.IsNullOrEmpty(value.AttemptedValue))
            {
                return bindingContext.ModelType == typeof (DateTime) ? new DateTime() : (DateTime?)null;
            }
            return DateTime.ParseExact(value.AttemptedValue, _customFormat, CultureInfo.InvariantCulture);
        }
    }
}