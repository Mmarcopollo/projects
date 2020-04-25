using System;
using System.Globalization;
using System.Windows.Controls;

namespace Zadanie4.ViewModel
{
    class RaitingValiddationRule : ValidationRule
    {
        public int min = 0;
        public int max = 6;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int x = Convert.ToInt32(value);
            if (x == null)
            {
                return new ValidationResult(false, "Please enter some vaule");
            }
            if ((x < min) || (x > max))
            {
                return new ValidationResult(false,
                  "Please enter an Rating in the range: " + min + " - " + max + ".");
            }
            else
            {
                return ValidationResult.ValidResult;
            }

        }
    }
}
