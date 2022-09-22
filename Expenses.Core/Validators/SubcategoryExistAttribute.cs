using System.ComponentModel.DataAnnotations;
using Expenses.Core.Interfaces.BusinessLayer;

namespace Expenses.Core.Validators
{
    /// <summary>
    /// Verify if category name exist
    /// </summary>
    public class SubcategoryExistAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool result;

            if(value is null)
                return new ValidationResult("La subcategoria no debe ser nula");
            var _unitOfWorkBl = validationContext.GetService(typeof(IUnitOfWork)) as IUnitOfWork;
            result = _unitOfWorkBl.Subcategory.Exists(value.ToString());
            if (result)
                return ValidationResult.Success;
            else
                return new ValidationResult("La subcategoria no existe");
        }
    }
}