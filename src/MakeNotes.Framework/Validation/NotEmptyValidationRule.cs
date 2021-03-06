﻿using System;
using System.Globalization;
using System.Windows.Controls;

namespace MakeNotes.Framework.Validation
{
    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return String.IsNullOrWhiteSpace(value?.ToString())
                ? new ValidationResult(false, "Field is required.")
                : ValidationResult.ValidResult;
        }
    }
}
