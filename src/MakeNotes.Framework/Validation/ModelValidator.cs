using System.ComponentModel.DataAnnotations;

namespace MakeNotes.Framework.Validation
{
    public static class ModelValidator
    {
        /// <summary>
        /// Determines whether the specified model is valid.
        /// </summary>
        /// <typeparam name="T">Type of the model.</typeparam>
        /// <param name="model">Instance of the model.</param>
        /// <returns>true if the object validates; otherwise, false.</returns>
        public static bool Validate<T>(T model)
        {
            if (model == null)
            {
                return false;
            }

            var validationContext = new ValidationContext(model, null, null);
            return Validator.TryValidateObject(model, validationContext, null, validateAllProperties: true);
        }
    }
}
