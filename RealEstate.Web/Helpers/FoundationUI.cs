using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;

namespace RealEstate.Web.Helpers
{
    /// <summary>
    /// This is useful if you use Foundation 4
    /// </summary>
    public static  class FoundationUI
    {
         #region Enums

        /// <summary>
        /// MVC has an InputType enumeration, but it is incomplete, so this
        /// leverages those values and extends it for HTML5 inputs.
        /// </summary>
        public enum HtmlInputType : int
        {
            /// <summary>
            /// A check box.
            /// </summary>
            CheckBox = 0,
            /// <summary>
            /// A hidden field.
            /// </summary>
            Hidden = 1,
            /// <summary>
            /// A password box.
            /// </summary>
            Password = 2,
            /// <summary>
            /// A radio button.
            /// </summary>
            Radio = 3,
            /// <summary>
            /// A text box.
            /// </summary>
            Text = 4,
            /// <summary>
            /// A select list
            /// </summary>
            Select = 5,
            /// <summary>
            /// A text area
            /// </summary>
            TextArea = 6,
            /// <summary>
            /// A button
            /// </summary>
            Button = 7,
            /// <summary>
            /// A submit button
            /// </summary>
            Submit = 8,
            /// <summary>
            /// An anchor
            /// </summary>
            Anchor = 9,
            /// <summary>
            /// The color
            /// </summary>
            Color = 10,
            /// <summary>
            /// The date
            /// </summary>
            Date = 11,
            /// <summary>
            /// The datetime
            /// </summary>
            Datetime = 12,
            /// <summary>
            /// The email
            /// </summary>
            Email = 13,
            /// <summary>
            /// The month
            /// </summary>
            Month = 14,
            /// <summary>
            /// The number
            /// </summary>
            Number = 15,
            /// <summary>
            /// The range
            /// </summary>
            Range = 16,
            /// <summary>
            /// The search
            /// </summary>
            Search = 17,
            /// <summary>
            /// The tel
            /// </summary>
            Tel = 18,
            /// <summary>
            /// The time
            /// </summary>
            Time = 19,
            /// <summary>
            /// The URL
            /// </summary>
            Url = 20,
            /// <summary>
            /// The week
            /// </summary>
            Week = 21
        }

        #endregion

        #region HtmlHelper Extensions

        /// <summary>
        /// Creates a generic input which can be used for any type of html input.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="inputType">Type of the input.</param>
        /// <param name="value">The value.</param>
        /// <param name="options">The options.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>IHtmlString</returns>
        public static IHtmlString GenericInputFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, HtmlInputType inputType,
            object htmlAttributes = null,
            Dictionary<string, string> options = null,
            bool includeLabel = true)
        {
            // Pull a reference to the model and the field's name and value
            var model = htmlHelper.ViewData.Model;
            var fieldValue = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).SimpleDisplayText;
            var fieldValueExact = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model;
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            
            // If we have html attributes in the anonymous object, we need to render them out
            StringBuilder propBuilder = new StringBuilder();
            if (htmlAttributes != null)
            {
                Type t = htmlAttributes.GetType();
                foreach (var prop in t.GetProperties())
                {
                    propBuilder.AppendFormat("{0}='{1}' ", prop.Name.Replace("_","-"), prop.GetValue(htmlAttributes, null));
                }
            }

            // Extract the attributes (Data Annotations)
            var required = model.GetAttributeFrom<RequiredAttribute>(fieldName);
            var maxLength = model.GetAttributeFrom<MaxLengthAttribute>(fieldName);
            var display = model.GetAttributeFrom<DisplayAttribute>(fieldName);
            var range = model.GetAttributeFrom<RangeAttribute>(fieldName);
            var dataType = model.GetAttributeFrom<DataTypeAttribute>(fieldName);
            var regex = model.GetAttributeFrom<RegularExpressionAttribute>(fieldName);
            var displayFormat = model.GetAttributeFrom<DisplayFormatAttribute>(fieldName);
            var custom = model.GetAttributeFrom<CustomValidationAttribute>(fieldName);

            // Parse the validation message
            var errorMessage = GenerateErrorMessage(required, regex, dataType, range, custom);

            // Used to determine whethere or not to generate the error field
            bool hasValidation = required != null || range != null || dataType != null || regex != null || custom != null;
            string formatString = null;
            var sb = new StringBuilder();
            string formattedFieldValue = displayFormat != null ? string.Format(displayFormat.DataFormatString, fieldValueExact) : fieldValue;
            
            // Based on the passed in input type, construct the html markup
            switch (inputType)
            {
                case HtmlInputType.Radio:
                case HtmlInputType.CheckBox:
                    formatString = includeLabel ? "<div class='name-field'><label class='margin-label'><input type='{1}' {2}{3}/>{0}</label></div>" : "<div class='name-field'><input type='{1}' {2}{3}/></div>";
                    sb.AppendFormat(formatString,
                        display != null ? display.Name : formattedFieldValue,
                        Enum.GetName(typeof(HtmlInputType), inputType).ToString().ToLower(),
                        propBuilder.ToString(),
                        !string.IsNullOrWhiteSpace(formattedFieldValue) && formattedFieldValue == "True" ? " checked " : string.Empty);
                    break;
                case HtmlInputType.Hidden:
                    sb.AppendFormat("<input type='{0}' {1}{2}/>",
                        Enum.GetName(typeof(HtmlInputType), inputType).ToString().ToLower(),
                        propBuilder.ToString(),
                        !string.IsNullOrWhiteSpace(formattedFieldValue) ? " value='" + HttpUtility.HtmlEncode(formattedFieldValue) + "'" : string.Empty);
                    break;
                case HtmlInputType.Button:
                case HtmlInputType.Password:
                case HtmlInputType.Submit:
                case HtmlInputType.Text:
                case HtmlInputType.Color:
                case HtmlInputType.Date:
                case HtmlInputType.Datetime:
                case HtmlInputType.Email:
                case HtmlInputType.Month:
                case HtmlInputType.Search:
                case HtmlInputType.Tel:
                case HtmlInputType.Time:
                case HtmlInputType.Url:
                case HtmlInputType.Week:
                case HtmlInputType.Number:
                case HtmlInputType.Range:
                    formatString = includeLabel ? "<div class='name-field'><label>{0} {8}<input type='{1}' {2}{3}{4}{5}{6}/></label>{7}</div>" : "<div class='name-field'><input type='{1}' {2}{3}{4}{5}{6}/>{7}</div>";
                    sb.AppendFormat(formatString,
                    /*0*/   display != null ? display.Name : formattedFieldValue,
                    /*1*/   Enum.GetName(typeof(HtmlInputType), inputType).ToString().ToLower(),
                    /*2*/   propBuilder.ToString(),
                    /*3*/   !string.IsNullOrWhiteSpace(formattedFieldValue) ? " value='" + HttpUtility.HtmlEncode(formattedFieldValue) + "'" : string.Empty,
                    /*4*/   required != null ? " required " : string.Empty,
                    /*5*/   regex != null ? " pattern='" + regex.Pattern + "' " : string.Empty,
                    /*6*/   maxLength != null ? " maxlength='" + maxLength.Length + "' " : string.Empty,
                    /*7*/   hasValidation ? "<small class='error'>" + errorMessage + "</small>" : string.Empty,
                    /*8*/   required != null ? "<small>Required</small>" : string.Empty);
                    break;
                case HtmlInputType.Select:
                    formatString = includeLabel ? "<div class='name-field'><label>{0} {5}<select {1}{2}>{3}</select>{4}</label></div>" : "<div class='name-field'><select {1}{2}>{3}</select>{4}</div>";
                    sb.AppendFormat(formatString,
                        display != null ? display.Name : formattedFieldValue,
                        propBuilder.ToString(),
                        required != null ? " required " : String.Empty,
                        options != null ? RenderOptions(options, formattedFieldValue) : string.Empty,
                        hasValidation ? "<small class='error'>" + errorMessage + "</small>" : string.Empty,
                        required != null ? "<small>Required</small>" : string.Empty);
                    break;
                case HtmlInputType.TextArea:
                    formatString = includeLabel ? "<div class='name-field'><label>{0} {7}<textarea {1}{2}{3}{4}>{5}</textarea></label>{6}</div>" : "<div class='name-field'><textarea {1}{2}{3}{4}>{5}</textarea>{6}</div>";
                    sb.AppendFormat(formatString,
                    /*0*/   display != null ? display.Name : formattedFieldValue,
                    /*1*/   propBuilder.ToString(),
                    /*2*/   required != null ? " required " : string.Empty,
                    /*3*/   regex != null ? " pattern='" + regex.Pattern + "' " : string.Empty,
                    /*4*/   maxLength != null ? " maxlength='" + maxLength.Length + "' " : string.Empty,
                    /*5*/   formattedFieldValue,
                    /*6*/   hasValidation ? "<small class='error'>" + errorMessage + "</small>" : string.Empty,
                    /*7*/   required != null ? "<small>Required</small>" : string.Empty);
                    break;
                case HtmlInputType.Anchor:
                    sb.AppendFormat("<a {0}>{1}</a>",
                       propBuilder.ToString(),
                       formattedFieldValue);
                    break;
            }

            return new HtmlString(sb.ToString());
        }

        #endregion HtmlHelper Extensions

        #region Private Methods

        /// <summary>
        /// Generates the error message.
        /// </summary>
        /// <param name="validation">The validation.</param>
        /// <param name="required">The required.</param>
        /// <param name="regex">The regex.</param>
        /// <param name="dataType">Type of the data.</param>
        /// <param name="range">The range.</param>
        /// <returns></returns>
        private static string GenerateErrorMessage(RequiredAttribute required, RegularExpressionAttribute regex, DataTypeAttribute dataType, RangeAttribute range, CustomValidationAttribute custom)
        {
            List<string> errorMessages = new List<string>();

            // If we have required set and message set, add to the list
            if (required != null && !string.IsNullOrWhiteSpace(required.ErrorMessage))
            {
                errorMessages.Add(required.ErrorMessage);
            }

            // If we have regex set and message set, add to the list
            if (regex != null && !string.IsNullOrWhiteSpace(regex.ErrorMessage))
            {
                errorMessages.Add(regex.ErrorMessage);
            }

            // If we have dataType set and message set, add to the list
            if (dataType != null && !string.IsNullOrWhiteSpace(dataType.ErrorMessage))
            {
                errorMessages.Add(dataType.ErrorMessage);
            }

            // If we have range set and message set, add to the list
            if (range != null && !string.IsNullOrWhiteSpace(range.ErrorMessage))
            {
                errorMessages.Add(range.ErrorMessage);
            }

            if (custom != null && !string.IsNullOrWhiteSpace(custom.ErrorMessage))
            {
                errorMessages.Add(custom.ErrorMessage);
            }

            // If we have messages, join them with a space, otherwise return nothing
            if (errorMessages.Count > 0)
                return String.Join(" ", errorMessages);
            else
                return string.Empty;
        }

        /// <summary>
        /// Renders the options for the select list.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="fieldValue">The field value.</param>
        /// <returns></returns>
        private static string RenderOptions(Dictionary<string, string> options, string fieldValue)
        {
            var sb = new StringBuilder();
            options.ToList().ForEach(opt =>
            {
                sb.AppendFormat("<option value='{0}'{1}>{2}</option>",
                    opt.Value,
                    opt.Value == fieldValue ? " selected " : "",
                    opt.Key);
            });
            return sb.ToString();
        }

        /// <summary>
        /// Gets the attribute from an object instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        private static T GetAttributeFrom<T>(this object instance, string propertyName) where T : Attribute
        {
            var attrType = typeof(T);
            var property = instance.GetType().GetProperty(propertyName);
            if (property != null)
            {
                return (T)property.GetCustomAttributes(attrType, false).FirstOrDefault();
            }
            else
            {
                return default(T);
            }
        }

        #endregion Private Methods

        public static Dictionary<string, string> CreateNullableBoolSelectOptions()
        {
            Dictionary<string, string> options = new Dictionary<string,string>();
            options.Add("Select...", String.Empty);
            options.Add("Yes", "True");
            options.Add("No", "False");
            return options;
        }
    
    }
}