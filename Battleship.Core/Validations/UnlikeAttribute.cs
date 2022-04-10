using System.ComponentModel.DataAnnotations;

namespace Battleship.Core.Validations;

[AttributeUsage(AttributeTargets.Property)]
public class UnlikeAttribute : ValidationAttribute
{
    private const string DefaultErrorMessage = "The value of {0} cannot be the same as the value of the {1}.";

    private string OtherProperty { get; }

    public UnlikeAttribute(string otherProperty)
        : base(DefaultErrorMessage)
    {
        if (string.IsNullOrEmpty(otherProperty))
        {
            throw new ArgumentNullException(nameof(otherProperty));
        }

        OtherProperty = otherProperty;
    }

    public override string FormatErrorMessage(string name)
    {
        return string.Format(ErrorMessageString, name, OtherProperty);
    }

    protected override ValidationResult? IsValid(object? value,
        ValidationContext validationContext)
    {
        if (value == null) return ValidationResult.Success;

        var otherProperty = validationContext.ObjectInstance.GetType()
            .GetProperty(OtherProperty);

        var otherPropertyValue = otherProperty?.GetValue(validationContext.ObjectInstance, null);

        if (value.Equals(otherPropertyValue))
        {
            return new ValidationResult(
                FormatErrorMessage(validationContext.DisplayName));
        }

        return ValidationResult.Success;
    }
}