using System.ComponentModel.DataAnnotations;
using Expenses.Core.Interfaces.BusinessLayer;
using MongoDB.Bson;

namespace Expenses.Core.Validators
{
    /// <summary>
    /// Verify if category name exist
    /// </summary>
    public class CategoryExistsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool result;
            var _unitOfWorkBl = validationContext.GetService(typeof(IUnitOfWork)) as IUnitOfWork;
            result = _unitOfWorkBl.Category.Exists(value.ToString());
            if (result)
                return ValidationResult.Success;
            else
                return new ValidationResult($"La categoria {value.ToString()} no existe");
        }
    }
}