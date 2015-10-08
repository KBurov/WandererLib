using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows.Markup;

namespace Wanderer.Library.Wpf.Markup
{
    /// <summary>
    /// Helps to use enumeration type as items source.
    /// </summary>
    public class EnumerationExtension : MarkupExtension
    {
        #region Variables
        private Type _enumType;
        #endregion

        /// <summary>
        /// Initialization constructor.
        /// </summary>
        /// <param name="enumType">enumeration type</param>
        public EnumerationExtension(Type enumType)
        {
            Contract.Requires<ArgumentNullException>(enumType != null, $"{nameof(enumType)} cannot be null");

            EnumType = enumType;
        }

        private Type EnumType
        {
            get { return _enumType; }
            set
            {
                if (_enumType == value)
                    return;

                var enumType = Nullable.GetUnderlyingType(value) ?? value;

                if (enumType.IsEnum == false)
                    throw new ArgumentException("Type must be an Enum.");

                _enumType = value;
            }
        }

        /// <summary>
        /// Returns array of <see cref="EnumerationMember"/> that corresponds target enum values.
        /// </summary>
        /// <param name="serviceProvider">a service provider helper that can provide services for the markup extension</param>
        /// <returns>array of <see cref="EnumerationMember"/> that corresponds target enum values</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var enumValues = Enum.GetValues(EnumType);

            return (from object enumValue in enumValues
                    select new EnumerationMember
                        {
                            Value = enumValue,
                            Description = GetDescription(enumValue)
                        }).ToArray();
        }

        private string GetDescription(object enumValue)
        {
            var descriptionAttribute = EnumType
                .GetField(enumValue.ToString())
                .GetCustomAttributes(typeof (DescriptionAttribute), false)
                .FirstOrDefault() as DescriptionAttribute;


            return descriptionAttribute != null
                ? descriptionAttribute.Description
                : enumValue.ToString();
        }

        private class EnumerationMember
        {
            public string Description { get; set; }

            public object Value { get; set; }
        }
    }
}