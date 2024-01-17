using System;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
namespace FSite.Models.Binders
{
    public class MyDateTimeModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var displayFormat = bindingContext.ModelMetadata.DisplayFormatString;
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            //  if (!string.IsNullOrEmpty(displayFormat) && value != null)

            if (string.IsNullOrEmpty(displayFormat))
            {
                displayFormat = "{0:dd/MM/yyyy h:mm tt}";
                //dd'/'MM'/'yyyy HH:mm:ss
            }
            if (!string.IsNullOrEmpty(displayFormat) && value != null)
            {
                DateTime date;
                displayFormat = displayFormat.Replace("{0:", string.Empty).Replace("}", string.Empty);
                // use the format specified in the DisplayFormat attribute to parse the date
                if (DateTime.TryParseExact(value.AttemptedValue, displayFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    return date;
                }
                else
                {
                    bindingContext.ModelState.AddModelError(
                        bindingContext.ModelName,
                        string.Format("{0} is an invalid date format", value.AttemptedValue)
                    );
                }
            }

            return base.BindModel(controllerContext, bindingContext);
        }
    }
    public class DateTimeModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, value);

            return value.ConvertTo(typeof(DateTime), CultureInfo.CurrentCulture);
        }
    }
    public class NullableDateTimeModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, value);

            return value == null
                ? null
                : value.ConvertTo(typeof(DateTime), CultureInfo.CurrentCulture);
        }
    }
}