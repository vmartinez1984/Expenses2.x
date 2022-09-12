using System.ComponentModel.DataAnnotations;
using Expenses.Core.Interfaces.BusinessLayer;

namespace Expenses.Core.Validators
{
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
                return new ValidationResult("La categoria no existe");
        }
    }
}