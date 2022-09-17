using System.ComponentModel.DataAnnotations;
using Expenses.Core.Interfaces.BusinessLayer;
using MongoDB.Bson;

namespace Expenses.Core.Validators
{
    /// <summary>
    /// Verify if periodId exists
    /// </summary>
    public class PeriodIdExistsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool result;

            if (ObjectId.TryParse(value.ToString(), out ObjectId id))
            {
                var _unitOfWorkBl = validationContext.GetService(typeof(IUnitOfWork)) as IUnitOfWork;
                result = _unitOfWorkBl.Period.Exists(value.ToString());
                if (result)
                    return ValidationResult.Success;
                else
                    return new ValidationResult("El periodo no existe");
            }
            else
            {
                return new ValidationResult("El periodo no existe, formato no valido");
            }
        }
    }
}