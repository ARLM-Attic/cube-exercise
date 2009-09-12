using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace CubeExercise
{
    public class NumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(
        object value, System.Globalization.CultureInfo cultureInfo)
        {
            int number;
            if (!int.TryParse(value as string, out number))
            {
                return new ValidationResult(
                    false,
                    "Invalid number format");
            }

            return ValidationResult.ValidResult;
        }
    }
}
